using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private float timeInterval;

    private Creature[] allCreatures;
    private bool[] locationStates;

    private int _totalCoins;
    public int TotalCoins => _totalCoins;

    private int countNE, countDD, countLC, countNT, countVU, countEN, countCR = 0;

    private Dictionary<ConservationStatus, int> coinValues = new Dictionary<ConservationStatus, int>()
    {
        {ConservationStatus.LC, 5},
        {ConservationStatus.NT, 10},
        {ConservationStatus.VU, 50},
        {ConservationStatus.EN, 250},
        {ConservationStatus.CR, 500},
        {ConservationStatus.NE, 300},
        {ConservationStatus.DD, 300}
    };

    void Start()
    {
        // Get all creatures
        allCreatures = UniversalManagers.instance.GetComponentInChildren<CreatureDatabase>().AllCreatures;

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
            float sum = (countLC * coinValues[ConservationStatus.LC]) +
                        (countNT * coinValues[ConservationStatus.NT]) +
                        (countVU * coinValues[ConservationStatus.VU]) +
                        (countEN * coinValues[ConservationStatus.EN]) +
                        (countCR * coinValues[ConservationStatus.CR]) +
                        (countNE * coinValues[ConservationStatus.NE]) +
                        (countDD * coinValues[ConservationStatus.DD]);

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
