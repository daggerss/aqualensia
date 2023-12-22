using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorBlink : MonoBehaviour
{
    [field: SerializeField]
    public int OnTimeLeft {get; private set;}

    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float speed;
    [SerializeField] private Image[] images;
    [SerializeField] private TMP_Text[] texts;

    public void Activate()
    {
        // Images
        LerpImages();

        // Text
        LerpTexts();
    }

    private void LerpImages()
    {
        foreach (Image img in images)
        {
            img.color = Color.Lerp(startColor, endColor,
                                     Mathf.PingPong(Time.time * speed, 1));
        }
    }

    private void LerpTexts()
    {
        foreach (TMP_Text txt in texts)
        {
            txt.color = Color.Lerp(startColor, endColor,
                                     Mathf.PingPong(Time.time * speed, 1));
        }
    }
}
