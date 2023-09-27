using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Cooldown airLevel;

    [Header("Ascent")]
    [SerializeField] private float ascendTime;
    [SerializeField] private float ascendSpeed;
    [SerializeField] private CinemachineConfiner2D vCamConfiner;
    [SerializeField] private CompositeCollider2D tileCollider;
    [SerializeField] private GameObject whiteScreen;
    [SerializeField] private Animator whiteScreenAnimation;

    [Header("Depth")]
    [SerializeField] private TMP_Text depthText;
    [SerializeField] private TMP_Text zoneText;

    private float depth;
    private float depthScale = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // Set up air level
        // TODO: Set new air level upgrade?
        airLevel.StartCooldown();

        // TODO: Set transform to exit point (for depth)
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAirLevel();
        UpdateDepth();
        UpdateOceanZone();
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
            StartCoroutine(Ascend());
        }
    }

    // Calculate depth
    private void UpdateDepth()
    {
        depth = (transform.position - player.transform.position).magnitude;
        depth *= depthScale;
        depthText.text = depth.ToString("F1") + " m";
    }

    // Determine ocean zone
    private void UpdateOceanZone()
    {
        if (0 <= depth && depth < 200)
        {
            zoneText.text = "Sunlight Zone";
        }
        else if (200 <= depth && depth < 1000)
        {
            zoneText.text = "Twilight Zone";
        }
        else if (1000 <= depth && depth < 4000)
        {
            zoneText.text = "Midnight Zone";
        }
        else if (4000 <= depth && depth < 6000)
        {
            zoneText.text = "Abyssal Zone";
        }
        else if (6000 <= depth && depth < 11000)
        {
            zoneText.text = "Hadal Zone";
        }
        else
        {
            zoneText.text = "—";
        }
    }

    IEnumerator Ascend()
    {
        // Fade to white
        whiteScreen.SetActive(true);
        whiteScreenAnimation.speed = 1 / ascendTime;
        whiteScreenAnimation.Play("FadeIn");

        // Disable movement + obstacles
        vCamConfiner.enabled = false;
        tileCollider.enabled = false;
        player.GetComponent<Movement>().enabled = false;

        // Ascend
        player.transform.Translate(Vector3.up * ascendSpeed * Time.deltaTime);
        yield return new WaitForSeconds(ascendTime);

        // Load scene
        Cursor.visible = true;
        SceneManager.LoadScene("Overworld");
    }
}
