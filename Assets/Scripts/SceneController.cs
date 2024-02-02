using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ! Note to Dagny
// Because I know your ass is gonna question this in the name of efficiency
// This is NOT under Universal Managers because I don't want the headache of
// finding its reference for Button OnClick() purposes

public class SceneController : MonoBehaviour
{
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
