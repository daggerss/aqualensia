using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewspaperDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text headlineText;
    [SerializeField] private Image photo;
    [SerializeField] private float delay;

    void OnEnable()
    {
        // Delay
        StartCoroutine(DelayInteractable());
    }

    // Set headline & photo
    public void SetDisplay(string headline, Sprite img)
    {
        // Headline
        headlineText.text = headline;

        // Photo
        photo.sprite = img;
    }

    IEnumerator DelayInteractable()
    {
        // Disable button
        GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(delay);

        // Enable button
        GetComponent<Button>().interactable = true;
    }
}
