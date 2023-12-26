using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogDisplay : MonoBehaviour
{
    [Header("Heading")]
    [SerializeField] private TMP_Text logNumText;

    [Header("Depth")]
    [SerializeField] private TMP_Text currentDepthText;
    [SerializeField] private GameObject currentDepthStar;
    [SerializeField] private TMP_Text previousDepthText;
    [SerializeField] private GameObject previousDepthStar;

    [Header("Shots")]
    [SerializeField] private TMP_Text currentShotsText;
    [SerializeField] private GameObject currentShotsStar;
    [SerializeField] private TMP_Text previousShotsText;
    [SerializeField] private GameObject previousShotsStar;

    [Header("Rarest")]
    [SerializeField] private Image rarestImage;
    [SerializeField] private TMP_Text rarestText;
    [SerializeField] private Image rarestCategoryImage;
    [SerializeField] private TMP_Text rarestCategoryText;

    [Header("Captured")]
    [SerializeField] private LogCapturedUI[] captured;

    [Header("Colors")]
    [SerializeField] private Color newBestColor;
    [SerializeField] private Color LCColor;
    [SerializeField] private Color NTColor;
    [SerializeField] private Color VUColor;
    [SerializeField] private Color ENColor;
    [SerializeField] private Color CRColor;
    [SerializeField] private Color NEColor;
    [SerializeField] private Color DDColor;

    // Set Log Num UI
    public void SetLogNum(int num)
    {
        logNumText.text = num.ToString();
    }

    // Set Depth UI
    public void SetDepth(float current, float previous)
    {
        currentDepthText.text = current.ToString();
        previousDepthText.text = previous.ToString();

        if (current > previous)
        {
            currentDepthText.color = newBestColor;
            currentDepthStar.SetActive(true);
            previousDepthStar.SetActive(false);
        }
        else
        {
            currentDepthText.color = new Color(255, 255, 255);
            currentDepthStar.SetActive(false);
            previousDepthStar.SetActive(true);
        }
    }

    // Set Shots UI
    public void SetShots(float current, float previous)
    {
        currentShotsText.text = current.ToString();
        previousShotsText.text = previous.ToString();

        if (current > previous)
        {
            currentShotsText.color = newBestColor;
            currentShotsStar.SetActive(true);
            previousShotsStar.SetActive(false);
        }
        else
        {
            currentShotsText.color = new Color(255, 255, 255);
            currentShotsStar.SetActive(false);
            previousShotsStar.SetActive(true);
        }
    }

    // Set Rarest UI
    public void SetRarest(Sprite img, string name, ConservationStatus category)
    {
        rarestImage.sprite = img;
        rarestText.text = name;

        if (category == ConservationStatus.LC)
        {
            rarestImage.color = LCColor;
            rarestCategoryText.text = "Least Concern";
        }

        else if (category == ConservationStatus.NT)
        {
            rarestImage.color = NTColor;
            rarestCategoryText.text = "Near Threatened";
        }

        else if (category == ConservationStatus.VU)
        {
            rarestImage.color = VUColor;
            rarestCategoryText.text = "Vulnerable";
        }

        else if (category == ConservationStatus.EN)
        {
            rarestImage.color = ENColor;
            rarestCategoryText.text = "Endangered";
        }

        else if (category == ConservationStatus.CR)
        {
            rarestImage.color = CRColor;
            rarestCategoryText.text = "Critically Endangered";
        }

        else if (category == ConservationStatus.NE)
        {
            rarestImage.color = NEColor;
            rarestCategoryText.text = "Not Evaluated";
        }

        else if (category == ConservationStatus.DD)
        {
            rarestImage.color = DDColor;
            rarestCategoryText.text = "Data Deficient";
        }
    }

    // Set Captured UI at Index
    public void SetCaptured(int index, Sprite img, int shots, bool isNew)
    {
        captured[index].SetImage(img);
        captured[index].SetShots(shots);
        captured[index].SetTag(isNew);
    }

    // Reset Captured UI at Index
    public void ResetCaptured(int index)
    {
        captured[index].Reset();
    }
}
