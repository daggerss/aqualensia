using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiveParallax : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed;

    private RawImage background;
    private Vector2 scrollPosition;

    void Start()
    {
        background = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        scrollPosition = transform.position * parallaxSpeed;
        background.uvRect = new Rect(scrollPosition, background.uvRect.size);
    }
}
