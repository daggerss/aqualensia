using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogDisplay : MonoBehaviour
{
    [Header("Parent UI")]
    [SerializeField] private GameObject[] logUI;
    [SerializeField] private GameObject arrowsUI;

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
    [SerializeField] private ConservationStatusUI categoryUI;

    [Header("Captured")]
    [SerializeField] private LogCapturedUI[] capturedCreatures;
    [SerializeField] private LogCapturedUI[] capturedBlockers;

    [Header("Colors")]
    [SerializeField] private Color newBestColor;

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

            // Show arrows
            if (diveLog.CapturedBlockers.Count > 0)
            {
                arrowsUI.SetActive(true);
            }

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

            arrowsUI.SetActive(false);
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
        if (c == null)
        {
            SetRarest(null, "No Creatures Captured");
        }

        else if (c.CaptureStatus == CreatureStatus.Identified)
        {
            SetRarest(c.Sprite, c.CommonName, c.ConservationStatus);
        }

        else // Do not reveal name if unidentified
        {
            SetRarest(c.Sprite, "???", c.ConservationStatus);
        }
    }

    private void UpdateCaptured()
    {
        // Creatures
        for (int i = 0; i < 7; i++)
        {
            try
            {
                // Get creature log
                CreatureLog currentLog = diveLog.CapturedCreatures[i];

                // Set item
                SetCapturedCreature(i, currentLog.CapturedCreature.Sprite,
                               currentLog.CaptureCount, currentLog.isNew);
            }

            catch // No log at index
            {
                // Reset item
                ResetCapturedCreature(i);
            }
        }

        // Blockers
        for (int i = 0; i < 14; i++)
        {
            try
            {
                // Get blocker log
                BlockerLog currentLog = diveLog.CapturedBlockers[i];

                // Set item
                SetCapturedBlocker(i, currentLog.CapturedBlocker.Sprites[0],
                               currentLog.CaptureCount, currentLog.isNew);
            }

            catch // No log at index
            {
                // Reset item
                ResetCapturedBlocker(i);
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
    private void SetRarest(Sprite img, string name,
                           ConservationStatus category = ConservationStatus.NE)
    {
        rarestImage.sprite = img;
        rarestImage.color = Color.white;
        rarestText.text = name;

        if (img != null) // Check if rarest exists
        {
            categoryUI.SetStatus(category);
        }

        else
        {
            rarestImage.color = Color.clear;
        }
    }

    // Set Captured Creature UI at Index
    private void SetCapturedCreature(int index, Sprite img, int shots, bool isNew)
    {
        capturedCreatures[index].SetImage(img);
        capturedCreatures[index].SetShots(shots);
        capturedCreatures[index].SetTag(isNew);
    }

    // Set Captured Blocker UI at Index
    private void SetCapturedBlocker(int index, Sprite img, int shots, bool isNew)
    {
        capturedBlockers[index].SetImage(img);
        capturedBlockers[index].SetShots(shots);
        capturedBlockers[index].SetTag(isNew);
    }

    // Reset Captured Creature UI at Index
    private void ResetCapturedCreature(int index)
    {
        capturedCreatures[index].Reset();
    }

    // Reset Captured Blocker UI at Index
    private void ResetCapturedBlocker(int index)
    {
        capturedBlockers[index].Reset();
    }
}
