using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Toast : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private ToastContent[] contentSets;

    [Header("UI")]
    [SerializeField] private Image bgImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text toastText;

    public void SetToast(int idx)
    {
        bgImage.color = contentSets[idx].bgColor;
        iconImage.sprite = contentSets[idx].icon;
        toastText.text = contentSets[idx].text;
    }
}
