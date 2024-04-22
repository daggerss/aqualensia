using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text coinValueText;
    [SerializeField] private float updateInterval;

    private CurrencyManager currencyManager;

    void OnEnable()
    {
        // Update UI
        StartCoroutine(UpdateUI());

        // Add update UI to deduct event
        CurrencyManager.OnDeduct += UpdateCurrency;
    }

    // Update text
    IEnumerator UpdateUI()
    {
        // Wait until no NRE
        while (currencyManager == null)
        {
            // Get currency manager
            if (UniversalManagers.instance != null)
            {
                currencyManager = UniversalManagers.instance.GetComponentInChildren<CurrencyManager>();
            }

            yield return new WaitForSeconds(0.05f);
        }

        // Update UI continuously
        while (true)
        {
            UpdateCurrency();

            yield return new WaitForSeconds(updateInterval);
        }
    }

    // Update Currency UI
    public void UpdateCurrency()
    {
        coinValueText.text = currencyManager.TotalCoins.ToString("N0");
    }
}
