using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text coinValueText;
    [SerializeField] private float updateInterval;

    private CurrencyManager currencyManager;
    private int totalCoins;

    void OnEnable()
    {
        // Update UI
        StartCoroutine(UpdateUI());
    }

    // Update text
    IEnumerator UpdateUI()
    {
        // Wait until after Start()
        if (UniversalManagers.instance == null)
        {
            yield return new WaitForSeconds(0.05f);

            // Get currency manager
            currencyManager = UniversalManagers.instance.GetComponentInChildren<CurrencyManager>();
        }

        // Update UI continuously
        while (true)
        {
            coinValueText.text = currencyManager.TotalCoins.ToString();
    
            yield return new WaitForSeconds(updateInterval);
        }
    }
}
