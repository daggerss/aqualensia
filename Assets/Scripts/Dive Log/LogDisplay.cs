using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogDisplay : MonoBehaviour
{
    [Header("Parent UI")]
    [SerializeField] private GameObject[] logUI;

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
    [SerializeField] private Color darkTextColor;
    [SerializeField] private Color LCColor;
    [SerializeField] private Color NTColor;
    [SerializeField] private Color VUColor;
    [SerializeField] private Color ENColor;
    [SerializeField] private Color CRColor;
    [SerializeField] private Color NEColor;
    [SerializeField] private Color DDColor;

    private LogManager diveLog;

    /* -------------------------------------------------------------------------- */
    /*                                    Setup                                   */
    /* -------------------------------------------------------------------------- */
    void Awake()
    {
        // Dive Log
        diveLog = UniversalManagers.instance.GetComponentInChildren<LogManager>();
    }

    void Start()
    {
        if (diveLog.ExitDive)
        {
            // Set log display
            UpdateLogNum();
            UpdateDepth();
            UpdateShots();
            UpdateRarest();
            UpdateCaptured();

            // Show UI
            foreach (GameObject ui in logUI)
            {
                ui.SetActive(true);
            }

            // Reset everything
            diveLog.ResetLog();
        }

        else
        {
            // Hide UI
            foreach (GameObject ui in logUI)
            {
                ui.SetActive(false);
            }
        }
    }

    /* -------------------------------------------------------------------------- */
    /*                                  Dive Log                                  */
    /* -------------------------------------------------------------------------- */

    private void UpdateLogNum()
    {
        // Update total dives
        diveLog.TotalDives += 1;
        
        // Set display
        SetLogNum(diveLog.TotalDives);
    }

    private void UpdateDepth()
    {
        // Set display
        SetDepth(diveLog.CurrentBestDepth, diveLog.PreviousBestDepth);

        // Update new best
        diveLog.UpdateDepth();
    }

    private void UpdateShots()
    {
        // Set display
        SetShots(diveLog.CurrentBestShots, diveLog.PreviousBestShots);

        // Update new best
        diveLog.UpdateShots();
    }

    private void UpdateRarest()
    {
        // Find rarest
        Creature c = diveLog.FindRarest();

        // Set display
        SetRarest(c.Sprite, c.CommonName, c.ConservationStatus);
    }

    private void UpdateCaptured()
    {
        // Go through entire list UI
        for (int i = 0; i < 7; i++)
        {
            try
            {
                // Get creature log
                CreatureLog currentLog = diveLog.CapturedCreatures[i];

                // Set item
                SetCaptured(i, currentLog.CapturedCreature.Sprite,
                               currentLog.CaptureCount, currentLog.isNew);
            }

            catch
            {
                // Reset item
                ResetCaptured(i);
            }
        }
    }

    /* -------------------------------------------------------------------------- */
    /*                                     UI                                     */
    /* -------------------------------------------------------------------------- */

    // Set Log Num UI
    private void SetLogNum(int num)
    {
        logNumText.text = num.ToString();
    }

    // Set Depth UI
    private void SetDepth(float current, float previous)
    {
        currentDepthText.text = current.ToString("F1") + "m";
        previousDepthText.text = previous.ToString("F1") + "m";

        if (current > previous)
        {
            currentDepthText.color = newBestColor;
            currentDepthStar.SetActive(true);
            previousDepthStar.SetActive(false);
        }
        else
        {
            currentDepthText.color = Color.white;
            currentDepthStar.SetActive(false);
            previousDepthStar.SetActive(true);
        }
    }

    // Set Shots UI
    private void SetShots(int current, int previous)
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
            currentShotsText.color = Color.white;
            currentShotsStar.SetActive(false);
            previousShotsStar.SetActive(true);
        }
    }

    // Set Rarest UI
    private void SetRarest(Sprite img, string name, ConservationStatus category)
    {
        rarestImage.sprite = img;
        rarestText.text = name;
        rarestCategoryText.color = darkTextColor;

        if (category == ConservationStatus.LC)
        {
            rarestCategoryImage.color = LCColor;
            rarestCategoryText.text = "Least Concern";
        }

        else if (category == ConservationStatus.NT)
        {
            rarestCategoryImage.color = NTColor;
            rarestCategoryText.text = "Near Threatened";
        }

        else if (category == ConservationStatus.VU)
        {
            rarestCategoryImage.color = VUColor;
            rarestCategoryText.text = "Vulnerable";
        }

        else if (category == ConservationStatus.EN)
        {
            rarestCategoryImage.color = ENColor;
            rarestCategoryText.color = Color.white;
            rarestCategoryText.text = "Endangered";
        }

        else if (category == ConservationStatus.CR)
        {
            rarestCategoryImage.color = CRColor;
            rarestCategoryText.color = Color.white;
            rarestCategoryText.text = "Critically Endangered";
        }

        else if (category == ConservationStatus.NE)
        {
            rarestCategoryImage.color = NEColor;
            rarestCategoryText.text = "Not Evaluated";
        }

        else if (category == ConservationStatus.DD)
        {
            rarestCategoryImage.color = DDColor;
            rarestCategoryText.text = "Data Deficient";
        }
    }

    // Set Captured UI at Index
    private void SetCaptured(int index, Sprite img, int shots, bool isNew)
    {
        captured[index].SetImage(img);
        captured[index].SetShots(shots);
        captured[index].SetTag(isNew);
    }

    // Reset Captured UI at Index
    private void ResetCaptured(int index)
    {
        captured[index].Reset();
    }
}
