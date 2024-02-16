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
    
    void Start()
    {
        // Get currency manager
        currencyManager = UniversalManagers.instance.GetComponentInChildren<CurrencyManager>();

        // Update UI
        StartCoroutine(UpdateUI());
    }

    // Update text
    IEnumerator UpdateUI()
    {
        while (true)
        {
            coinValueText.text = currencyManager.TotalCoins.ToString();

            yield return new WaitForSeconds(updateInterval);
        }
    }
}
