using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureFlee : MonoBehaviour
{
    private CreatureInDive creatureInstance;

    void Start()
    {
        // Get creature
        creatureInstance = GetComponent<CreatureInDive>();
    }

    // ! Note to self: the following two methods also consider scene camera
    void OnBecameVisible()
    {
        PhotoCapture.OnPhotoCapture += Flee;
    }

    void OnBecameInvisible()
    {
        PhotoCapture.OnPhotoCapture -= Flee;
    }

    // Speed away
    private void Flee()
    {
        // Check if mobile
        if (!creatureInstance.Creature.Sessile)
        {
            StartCoroutine(creatureInstance.Flee());
        }
    }
}
