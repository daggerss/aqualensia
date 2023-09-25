using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationSelector : MonoBehaviour
{
    [SerializeField] private string locationCode;

    public void OpenScene()
    {
        SceneManager.LoadScene(locationCode);
    }
}
