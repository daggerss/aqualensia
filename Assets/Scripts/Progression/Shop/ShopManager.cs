using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] DialogueDisplay fundDisplay;
    [SerializeField] NewspaperDisplay newsDisplay;

    private StateManager stateManager;
    private CurrencyManager currencyManager;

    public PamphletDisplay CurrentPamphlet {get; set;}
    private ParentBlocker currentBlocker;

    void Start()
    {
        // Get components
        stateManager = UniversalManagers.instance.GetComponentInChildren<StateManager>();
        currencyManager = UniversalManagers.instance.GetComponentInChildren<CurrencyManager>();
    }

    // Set pamphlet dialogue details
    public void ShowFundDialogue()
    {
        if (CurrentPamphlet != null)
        {
            currentBlocker = CurrentPamphlet.Blocker;

            // Project Description
            fundDisplay.SetParagraph(currentBlocker.ShopInfo);
    
            // Issue + Location
            string[] emphasisInfo = {currentBlocker.Name, currentBlocker.LocationName};
            fundDisplay.SetEmphasis(emphasisInfo);
    
            // Cost
            bool insufficientFunds = currencyManager.TotalCoins < currentBlocker.Cost;
            fundDisplay.SetCost(currentBlocker.Cost, insufficientFunds);
    
            // Reveal
            fundDisplay.gameObject.SetActive(true);
        }
    }

    // Set newspaper details
    public void ShowNewspaper()
    {
        if (CurrentPamphlet != null)
        {
            currentBlocker = CurrentPamphlet.Blocker;

            // Set details
            newsDisplay.SetDisplay(currentBlocker.Headline, currentBlocker.NewsPhoto);
    
            // Reveal
            newsDisplay.gameObject.SetActive(true);
        }
    }

    // "Buy" project
    public void FundProject()
    {
        // Deduct money
        currencyManager.Deduct(currentBlocker.Cost);

        // Restore location
        stateManager.LocationBlockStates[currentBlocker.LocationCode] = false;

        // Display newspaper
        ShowNewspaper();

        // Reset pamphlet
        CurrentPamphlet.ResetNormalState();

        // Set pamphlet to complete
        CurrentPamphlet.SetCompleteState(true);

        // Hide fund dialogue
        fundDisplay.gameObject.SetActive(false);
    }

    // Deselect pamphlet
    public void CancelFund()
    {
        // Reset button state
        CurrentPamphlet.ResetNormalState();

        // Hide fund dialogue
        fundDisplay.gameObject.SetActive(false);
    }
}
