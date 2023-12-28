using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    // Scene Check
    [HideInInspector]
    public bool ExitDive = false;

    // Dive No.
    public int TotalDives {get; set;}

    // Depth
    public float PreviousBestDepth {get; private set;}
    public float CurrentBestDepth {get; set;}

    // Shots
    public int PreviousBestShots {get; private set;}
    public int CurrentBestShots {get; set;}

    // Rarest Creature
    private Creature rarestCreature;

    // Captured
    [HideInInspector]
    public List<CreatureLog> CapturedCreatures = new List<CreatureLog>();

    /* -------------------------------------------------------------------------- */
    /*                                  Functions                                 */
    /* -------------------------------------------------------------------------- */

    // Update best depth
    public void UpdateDepth()
    {
        if (CurrentBestDepth > PreviousBestDepth)
        {
            PreviousBestDepth = CurrentBestDepth;
        }
    }

    // Update best shots
    public void UpdateShots()
    {
        if (CurrentBestShots > PreviousBestShots)
        {
            PreviousBestShots = CurrentBestShots;
        }
    }

    // Find Rarest
    public Creature FindRarest()
    {
        // Defaults
        int currentRarestStatus = 0;
        rarestCreature = CapturedCreatures[0].CapturedCreature;

        // Compare
        foreach (CreatureLog log in CapturedCreatures)
        {
            Creature c = log.CapturedCreature;

            if ((int)c.ConservationStatus > currentRarestStatus)
            {
                currentRarestStatus = (int)c.ConservationStatus;
                rarestCreature = c;
            }
        }

        return rarestCreature;
    }

    // Reset values
    public void ResetLog()
    {
        ExitDive = false;
        CurrentBestDepth = 0;
        CurrentBestShots = 0;
        rarestCreature = null;

        // Deletion just in case
        for (int i = 0; i < CapturedCreatures.Count; i++)
        {
            CapturedCreatures[i] = null;
        }

        CapturedCreatures.Clear();
    }
}
