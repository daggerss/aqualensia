using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationBuoy : MonoBehaviour
{
    [SerializeField] private GameObject flag;

    // Show flag
    public void ShowFlag(bool show)
    {
        flag.SetActive(show);
    }
}
