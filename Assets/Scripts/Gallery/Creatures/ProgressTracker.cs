using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressTracker : MonoBehaviour
{
    public int Count {get; private set;}
    public int Total {get; private set;}

    [SerializeField] private TMP_Text progressText;
    [SerializeField] private Image progressBar;

    [Header("Magic Word")]
    [SerializeField] private GameObject magicWord;

    // Set parameters
    public void Initialize(int count, int total)
    {
        Count = count;
        Total = total;

        SetCount();
        SetFill((float) Count / Total);
    }

    // Update by adding one
    public void AddOne()
    {
        Count++;
        SetCount();
        SetFill((float) Count / Total);

        CheckCompletion();
    }

    // Set text in format "N/T"
    private void SetCount()
    {
        progressText.text = Count.ToString() + "/" + Total.ToString();;
    }

    // Set bar (between 0 - 1)
    private void SetFill(float amount)
    {
        progressBar.fillAmount = amount;
    }

    // Checks 100% in biome
    // for Magic Word
    private void CheckCompletion()
    {
        if (Count / Total == 1)
        {
            // MAGIC WORD
            // SFX
            AudioManager audioManager = UniversalManagers.instance.GetComponentInChildren<AudioManager>();
            audioManager.PlaySFX("Magic Word");

            // Reveal
            magicWord.SetActive(true);
        }
    }
}
