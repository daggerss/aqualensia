using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ParallaxZoom : MonoBehaviour
{
    private RawImage background;
    private Vector2 minSize;
    private Vector2 maxSize;
    private Vector2 zoomFactor;
    private Vector2 zoomSize;
    private float scroll;

    [SerializeField] private float zoomMultiplier;
    [SerializeField] private float maxZoom;
    [SerializeField] private Vector2 zoomSpeed;
    [SerializeField] private float smoothTime;

    void Start()
    {
        background = GetComponent<RawImage>();
        maxSize = background.uvRect.size;
        minSize = maxSize * maxZoom;
    }

    void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
        zoomFactor = Vector2.one * (scroll * zoomMultiplier);
        zoomSize = background.uvRect.size - zoomFactor;
        zoomSize = ClampScale(zoomSize);
        zoomSize = Vector2.SmoothDamp(background.uvRect.size, zoomSize, ref zoomSpeed, smoothTime);
        background.uvRect = new Rect(background.uvRect.position, zoomSize);
    }

    // Clamp to original and max values
    private Vector2 ClampScale(Vector2 size)
    {
        if ((size.x < minSize.x) || (size.y < minSize.y))
        {
            size = minSize;
        }

        if ((size.x > maxSize.x) || (size.y > maxSize.y))
        {
            size = maxSize;
        }

        return size;
    }
}
