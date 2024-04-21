using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] DialogueDisplay fundDisplay;

    public PamphletDisplay CurrentPamphlet {get; set;}

    // Set pamphlet dialogue details
    public void ShowFundDialogue()
    {
        if (CurrentPamphlet != null)
        {
            ParentBlocker blocker = CurrentPamphlet.Blocker;

            // Project Description
            fundDisplay.SetParagraph(blocker.ShopInfo);
    
            // Issue + Location
            string[] emphasisInfo = {blocker.Name, blocker.Location};
            fundDisplay.SetEmphasis(emphasisInfo);
    
            // Cost
            fundDisplay.SetCost(blocker.Cost);
    
            // Reveal
            fundDisplay.gameObject.SetActive(true);
        }
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
