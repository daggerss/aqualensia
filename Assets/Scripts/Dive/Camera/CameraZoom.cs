using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    private float zoom;
    private float scroll;
    private CinemachineVirtualCamera vCam;

    [SerializeField] private float zoomMultiplier;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float smoothTime;

    // Start is called before the first frame update
    void Start()
    {
        // Get virtual cam
        CinemachineBrain cinemachineBrain = (Camera.main == null) ? null : Camera.main.GetComponent<CinemachineBrain>();
        vCam = (cinemachineBrain == null) ? null : cinemachineBrain.ActiveVirtualCamera as CinemachineVirtualCamera;

        if (vCam == null) // Error
        {
            Debug.Log("Main Camera, Cinemachine Brain, or Virtual Camera is null!");
        }
        else // Set current
        {
            zoom = vCam.m_Lens.OrthographicSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);  
        vCam.m_Lens.OrthographicSize = Mathf.SmoothDamp(vCam.m_Lens.OrthographicSize,
                                                        zoom, ref zoomSpeed, smoothTime);
    }
}
