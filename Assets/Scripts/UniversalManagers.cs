using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalManagers : MonoBehaviour
{
    public static UniversalManagers instance;

    void Awake()
    {
        // Destroy duplicate instances
        if (instance == null)
        {
            instance = this;

            // Keep managers between scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
