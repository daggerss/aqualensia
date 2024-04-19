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
            coinValueText.text = currencyManager.TotalCoins.ToString();

            yield return new WaitForSeconds(updateInterval);
        }
    }
}
