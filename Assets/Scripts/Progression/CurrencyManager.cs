using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private float timeInterval;

    [Header("Values")]
    [SerializeField] private int valueLC;
    [SerializeField] private int valueNT;
    [SerializeField] private int valueVU;
    [SerializeField] private int valueEN;
    [SerializeField] private int valueCR;
    [SerializeField] private int valueNE;
    [SerializeField] private int valueDD;
    [SerializeField] private int valueBlocker;

    private Creature[] allCreatures;
    private ParentBlocker[] capturedBlockers;
    private bool[] locationStates;

    private int _totalCoins;
    public int TotalCoins => _totalCoins;

    private int countNE, countDD, countLC, countNT, countVU, countEN, countCR = 0;

    void Start()
    {
        // Get all creatures
        allCreatures = UniversalManagers.instance.GetComponentInChildren<CreatureDatabase>().AllCreatures;

        // Get captured blockers
        capturedBlockers = UniversalManagers.instance.GetComponentInChildren<BlockerDatabase>().GetCapturedBlockers();

        // Get each location's block state
        locationStates = UniversalManagers.instance.GetComponentInChildren<StateManager>()
                                                   .LocationBlockStates.Values.ToArray();
        
        // Set up existing currency
        InitializeCoins();

        // Update
        StartCoroutine(UpdateTotal());
    }

    // Initialize from latest creatures
    private void InitializeCoins()
    {
        foreach (Creature creature in allCreatures)
        {
            if (creature.CaptureStatus == CreatureStatus.Identified)
            {
                AddToCount(creature.ConservationStatus);
            }
        }
    }

    // Add identified creature to count
    public void AddToCount(ConservationStatus status)
    {
        if (status == ConservationStatus.LC)
        {
            countLC++;
        }

        else if (status == ConservationStatus.NT)
        {
            countNT++;
        }

        else if (status == ConservationStatus.VU)
        {
            countVU++;
        }

        else if (status == ConservationStatus.EN)
        {
            countEN++;
        }

        else if (status == ConservationStatus.CR)
        {
            countCR++;
        }

        else if (status == ConservationStatus.NE)
        {
            countNE++;
        }

        else if (status == ConservationStatus.DD)
        {
            countDD++;
        }
    }

    // Update total coins
    IEnumerator UpdateTotal()
    {
        while (true)
        {
            // Calculate per identified creature
            float sum = (countLC * valueLC) + (countNT * valueNT) +
                        (countVU * valueVU) + (countEN * valueEN) +
                        (countCR * valueCR) + (countNE * valueNE) +
                        (countDD * valueDD);

            // Calculate per fully captured blocker
            sum += capturedBlockers.Length * valueBlocker;

            // Cumulative cut of 20% for every restored location
            foreach (bool isBlocked in locationStates)
            {
                if (!isBlocked)
                {
                    sum = sum * 0.20f;
                }
            }
            
            // Add to total
            _totalCoins += (int) sum;

            // Wait
            yield return new WaitForSeconds(timeInterval);
        }
    }
}
