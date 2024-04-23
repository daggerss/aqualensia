using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [Header("Air")]
    [SerializeField] private Cooldown airLevel;
    [SerializeField] private TMP_Text airText;
    [SerializeField] private ColorBlink[] dangerBlinks;

    [Header("Depth")]
    [SerializeField] private float depthScale = 0.5f;
    [SerializeField] private TMP_Text depthText;

    [Header("Zone")]
    [SerializeField] private Image zoneIcon;
    [SerializeField] private TMP_Text zoneText;
    [SerializeField] private Sprite[] zoneSprites;

    [Header("Ascent")]
    [SerializeField] private SceneController sceneController;
    [SerializeField] private float ascendTime;
    [SerializeField] private float ascendSpeed;
    [SerializeField] private GameObject whiteScreen;
    [SerializeField] private Animator whiteScreenAnimation;

    [Header("Disable on Ascent")]
    [SerializeField] private CinemachineConfiner2D vCamConfiner;
    [SerializeField] private CompositeCollider2D tileCollider;

    private int airLeft;
    private float depth;

    private LogManager diveLog;
    private float deepestDepth = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Set up air level
        airLevel.SetCooldownTime(PlayerPrefs.GetFloat("TankCapacity", 90));
        airLevel.StartCooldown();

        // Dive Log
        diveLog = UniversalManagers.instance.GetComponentInChildren<LogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAirLevel();
        UpdateDepth();
        UpdateDeepestDepth();
        UpdateOceanZone();
    }

    // Air level manager
    private void UpdateAirLevel()
    {
        if (airLevel.IsCoolingDown)
        {
            // Text
            airLeft = (int)(airLevel.CooldownTime - Time.timeSinceLevelLoad);
            airText.text = airLeft.ToString();

            // Radial
            airLevel.UpdateRadial();

            // Danger UI
            foreach (ColorBlink blink in dangerBlinks)
            {
                if(airLeft <= blink.OnTimeLeft)
                {
                    blink.Activate();
                }
            }
        }

        if (!airLevel.IsCoolingDown)
        {
            airLeft = 0;
            StartCoroutine(Ascend());
        }
    }

    // Calculate depth
    private void UpdateDepth()
    {
        depth = transform.position.y - player.transform.position.y;
        depth *= depthScale;
        depthText.text = depth.ToString("F1") + "m";
    }

    // Determine ocean zone
    private void UpdateOceanZone()
    {
        if (0 <= depth && depth < 200)
        {
            zoneIcon.sprite = zoneSprites[0];
            zoneText.text = "Sunlight Zone";
        }
        else if (200 <= depth && depth < 1000)
        {
            zoneIcon.sprite = zoneSprites[1];
            zoneText.text = "Twilight Zone";
        }
        else if (1000 <= depth && depth < 4000)
        {
            zoneIcon.sprite = zoneSprites[2];
            zoneText.text = "Midnight Zone";
        }
        else if (4000 <= depth && depth < 6000)
        {
            zoneIcon.sprite = zoneSprites[3];
            zoneText.text = "Abyssal Zone";
        }
        else if (6000 <= depth && depth < 11000)
        {
            zoneIcon.sprite = zoneSprites[3];
            zoneText.text = "Hadal Zone";
        }
        else
        {
            zoneText.text = "—";
        }
    }

    // Update deepest depth
    private void UpdateDeepestDepth()
    {
        if (depth > deepestDepth)
        {
            // Update here
            deepestDepth = depth;

            // Update log
            diveLog.CurrentBestDepth = deepestDepth;
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
        player.GetComponent<Rigidbody2D>().simulated = false;

        // Ascend
        player.transform.Translate(Vector3.up * ascendSpeed * Time.deltaTime);
        yield return new WaitForSeconds(ascendTime);

        // Set up + Load scene
        diveLog.ExitDive = true;
        Cursor.visible = true;
        sceneController.OpenScene("Overworld");
    }
}
