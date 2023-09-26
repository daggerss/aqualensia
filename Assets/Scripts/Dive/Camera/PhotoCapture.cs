using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Viewfinder")]
    [SerializeField] private GameObject viewfinder;
    [SerializeField] private float aimXOffset;
    [SerializeField] private float aimYOffset;
    [SerializeField] private Cooldown cooldown;

    [Header("Photo Display")]
    [SerializeField] private Image photoDisplay;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private float displayTime;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fade Effect")]
    [SerializeField] private Animator displayAnimation;
    [SerializeField] private Animator frameAnimation;

    private AudioManager audioManager;

    private Texture2D screenCapture;
    private int photoWidth, photoHeight, viewWidth, viewHeight;
    private bool viewingPhoto;

    private Vector3 viewMousePos;
    private Vector3 photoMousePos;

    // Set up screenshot
    void Start()
    {
        // Hide cursor
        Cursor.visible = false;

        // Audio
        audioManager = UniversalManagers.instance.GetComponentInChildren<AudioManager>();

        // Viewfinder size
        RectTransform tempRectTransform = viewfinder.GetComponent<RectTransform>();

        viewWidth = (int)(tempRectTransform.rect.width);
        viewHeight = (int)(tempRectTransform.rect.height);

        // Photo details
        tempRectTransform = photoDisplay.GetComponent<RectTransform>();

        photoWidth = (int)(tempRectTransform.rect.width);
        photoHeight = (int)(tempRectTransform.rect.height);

        screenCapture = new Texture2D(photoWidth, photoHeight, TextureFormat.RGB24, false);
    }

    void Update()
    {
        // Aim
        AimCamera();

        // Capture
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) &&
            !viewingPhoto && !cooldown.IsCoolingDown)
        {
            StartCoroutine(CapturePhoto());
            cooldown.StartCooldown();
        }

        // Cooldown
        if (cooldown.IsCoolingDown)
        {
            cooldown.UpdateSlider();
        }
    }

    // Take screenshot
    IEnumerator CapturePhoto()
    {
        // Hide viewfinder
        viewfinder.SetActive(false);

        yield return new WaitForEndOfFrame();

        int x = (int)photoMousePos.x - (photoWidth / 2);
        int y = (int)photoMousePos.y - (photoHeight / 2);

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
        // Show all
        viewingPhoto = true;
        photoFrame.GetComponent<CanvasGroup>().alpha = 1;
        photoFrame.SetActive(true);
        audioManager.PlaySFX("Shutter");
        cameraFlash.SetActive(true);
        displayAnimation.Play("FadeIn");
        viewfinder.SetActive(true);

        // Flash wait + hide
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);

        // Photo fade out
        yield return new WaitForSeconds(displayTime / 2);
        frameAnimation.Play("FadeOut");

        // Photo wait + hide
        yield return new WaitForSeconds(displayTime / 2);
        photoFrame.SetActive(false);
        viewingPhoto = false;
    }

    // Aim
    void AimCamera()
    {
        // Mouse position for viewfinder
        viewMousePos = ClampMousePosition(viewWidth, viewHeight);
        viewMousePos = Camera.main.ScreenToWorldPoint(viewMousePos);
        viewMousePos.z = 1.0f;

        // Mouse position for photo
        photoMousePos = ClampMousePosition(photoWidth, photoHeight);
        photoMousePos.z = 1.0f;

        // Set viewfinder position
        viewfinder.transform.position = viewMousePos;
    }

    // Get mouse position within screen
    Vector3 ClampMousePosition(float xReference, float yReference)
    {
        float x = Mathf.Clamp(Input.mousePosition.x,
                            (xReference / 2) + aimXOffset,
                            Screen.width - (xReference / 2) - aimXOffset);
        float y = Mathf.Clamp(Input.mousePosition.y,
                                      (yReference / 2) + aimYOffset,
                                      Screen.height - (yReference / 2) - aimYOffset);
        float z = Input.mousePosition.z;

        return new Vector3(x, y, z);
    }
}
