using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Cooldown airLevel;
    [SerializeField] private TMP_Text depthText;

    private float depth;
    private float depthScale = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Set new air level upgrade?
        
        airLevel.StartCooldown();

        // TODO: Set transform to exit point
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAirLevel();
        UpdateDepth();
    }

    // Air level manager
    private void UpdateAirLevel()
    {
        if (airLevel.IsCoolingDown)
        {
            airLevel.UpdateRadial();
        }

        if (!airLevel.IsCoolingDown)
        {
            Cursor.visible = true;
            SceneManager.LoadScene("Overworld");
        }
    }

    // Calculate depth
    private void UpdateDepth()
    {
        depth = (transform.position - player.transform.position).magnitude;
        depth *= depthScale;
        depthText.text = depth.ToString("F1") + " m";
    }
}
