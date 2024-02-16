using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private float timeInterval;

    private int _totalCoins;
    public int TotalCoins => _totalCoins;

    [SerializeField] private int countNE, countDD, countLC, countNT, countVU, countEN, countCR = 0;

    private Dictionary<ConservationStatus, int> coinValues = new Dictionary<ConservationStatus, int>()
    {
        {ConservationStatus.LC, 5},
        {ConservationStatus.NT, 10},
        {ConservationStatus.VU, 50},
        {ConservationStatus.EN, 250},
        {ConservationStatus.CR, 1000},
        {ConservationStatus.NE, 500},
        {ConservationStatus.DD, 500}
    };

    void Awake()
    {
        StartCoroutine(UpdateTotal());
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
            _totalCoins += (countLC * coinValues[ConservationStatus.LC]) +
                           (countNT * coinValues[ConservationStatus.NT]) +
                           (countVU * coinValues[ConservationStatus.VU]) +
                           (countEN * coinValues[ConservationStatus.EN]) +
                           (countCR * coinValues[ConservationStatus.CR]) +
                           (countNE * coinValues[ConservationStatus.NE]) +
                           (countDD * coinValues[ConservationStatus.DD]);

            yield return new WaitForSeconds(timeInterval);
        }
    }
}
