using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplay;
    [SerializeField] private GameObject photoFrame;

    private Texture2D screenCapture;
    private int photoWidth, photoHeight;
    
    // Set up screenshot
    void Start()
    {
        RectTransform photoRectTransform = photoDisplay.GetComponent<RectTransform>();
        float cropSize = 2;

        photoWidth = (int)(photoRectTransform.rect.width / cropSize);
        photoHeight = (int)(photoRectTransform.rect.height / cropSize);

        screenCapture = new Texture2D(photoWidth, photoHeight, TextureFormat.RGB24, false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CapturePhoto());
        }
    }

    // Take screenshot
    IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();

        int x = (Screen.width / 2) - (photoWidth / 2);
        int y = (Screen.height / 2) - (photoHeight / 2) - (int)photoDisplay.transform.position.y;

        Rect regionToRead = new Rect(x, y, photoWidth, photoHeight);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();

        CreatePhoto();
    }

    // Create photo sprite
    void CreatePhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplay.sprite = photoSprite;

        StartCoroutine(DisplayPhoto());
    }

    // Display and hide photo
    IEnumerator DisplayPhoto()
    {
        photoFrame.SetActive(true);
        yield return new WaitForSeconds(2);
        photoFrame.SetActive(false);
    }
}
