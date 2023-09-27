using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private float zoom = -1f;
    private float scroll;
    private GameObject mainCam;
    private CinemachineBrain brain;
    private CinemachineVirtualCamera vCam;

    [SerializeField] private float zoomMultiplier;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float smoothTime;

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
        scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);  
        vCam.m_Lens.OrthographicSize = Mathf.SmoothDamp(vCam.m_Lens.OrthographicSize,
                                                        zoom, ref zoomSpeed, smoothTime);
    }
}
