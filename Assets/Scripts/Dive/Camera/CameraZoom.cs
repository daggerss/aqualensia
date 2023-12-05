using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    private float zoom = -1f;
    private float scroll;
    private float zoomDamp;
    private GameObject zoomUI;
    private bool isFirstValueChange;
    private bool isSliderCoroutineRunning;
    private GameObject mainCam;
    private CinemachineBrain brain;
    private CinemachineVirtualCamera vCam;

    [SerializeField] private float zoomMultiplier;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float smoothTime;
    [SerializeField] private Slider zoomSlider;

    CinemachineVirtualCamera ActiveVirtualCamera
    {
        get { return brain == null ? null : brain.ActiveVirtualCamera as CinemachineVirtualCamera; }
    }

    void Awake()
    {
        mainCam = GameObject.Find("Main Camera");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get virtual cam
        brain = mainCam.GetComponent<CinemachineBrain>();
        vCam = ActiveVirtualCamera;

        if (vCam != null) // Baka sakali
        {
            zoom = vCam.m_Lens.OrthographicSize;
        }

        // Set up slider
        zoomSlider.minValue = minZoom;
        zoomSlider.maxValue = maxZoom;
        zoomUI = zoomSlider.transform.parent.gameObject;
        isFirstValueChange = true;
        isSliderCoroutineRunning = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get virtual cam again
        if (vCam == null)
        {
            vCam = ActiveVirtualCamera;
        }
        // Zoom proper
        else
        {
            if (zoom <= -1f)
            {
                zoom = vCam.m_Lens.OrthographicSize;
            }

            ZoomCam();
        }
    }

    private void ZoomCam()
    {
        // Camera
        scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        zoomDamp = Mathf.SmoothDamp(vCam.m_Lens.OrthographicSize, zoom,
                                    ref zoomSpeed, smoothTime);
        vCam.m_Lens.OrthographicSize = zoomDamp;

        // UI
        zoomSlider.value = zoomDamp;
    }

    public void DisplaySlider()
    {
        if (zoomUI == null)
        {
            zoomUI = zoomSlider.transform.parent.gameObject;
        }
        else if (isFirstValueChange)
        {
            isFirstValueChange = false;
        }
        else
        {
            if (!isSliderCoroutineRunning && !isFirstValueChange)
            {
                StartCoroutine(ActivateSlider());
            }
        }
    }

    IEnumerator ActivateSlider()
    {
        // Show
        isSliderCoroutineRunning = true;
        zoomUI.SetActive(true);

        // Hide
        yield return new WaitForSeconds(3f);
        zoomUI.SetActive(false);
        isSliderCoroutineRunning = false;
    }
}
