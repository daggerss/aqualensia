using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapManager : MonoBehaviour
{
    [SerializeField] private LocationDetails[] locations;

    void Start()
    {
        // Sort locations by order
        locations = locations.OrderBy(loc => loc.Order).ToArray();

        // Get num of unlocked
        int unlockedLocations = PlayerPrefs.GetInt("UnlockedLocations", 1);

        // Clear screen
        ClearLocations();

        // Show locations
        UnlockLocations(unlockedLocations);
    }

    // Clear all locations
    private void ClearLocations()
    {
        for (int i = 0; i < locations.Length; i++)
        {
            locations[i].Buoy.gameObject.SetActive(false);
        }
    }

    // Show unlocked locations
    private void UnlockLocations(int num)
    {
        for (int i = 0; i < num; i++)
        {
            // Show
            locations[i].Buoy.gameObject.SetActive(true);

            // Check for unknown
            bool hasUnknown;
            
            if (PlayerPrefs.GetInt("NightDive", 0) == 1) // + Night Creatures
            {
                hasUnknown = UniversalManagers.instance
                                              .GetComponentInChildren<CreatureDatabase>()
                                              .GetCreatures(locations[i].Code)
                                              .Any(c => c.CaptureStatus == CreatureStatus.Unknown);
            }

            else // Day Creatures only
            {
                hasUnknown = UniversalManagers.instance
                                              .GetComponentInChildren<CreatureDatabase>()
                                              .GetCreatures(locations[i].Code)
                                              .Any(c => (c.CaptureStatus == CreatureStatus.Unknown)
                                                     && (c.ActiveTime == TimeOfDay.Day
                                                     ||  c.ActiveTime == TimeOfDay.Both));
            }

            // Show/hide flag
            locations[i].Buoy.ShowFlag(hasUnknown);
        }
    }
}
