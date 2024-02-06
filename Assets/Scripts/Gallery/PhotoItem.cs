using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoItem : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private Sprite coralReefBG;
    [SerializeField] private Sprite seagrassBedBG;
    [SerializeField] private Sprite openOceanBG;

    [Header("UI")]
    [SerializeField] private Image BG;
    [SerializeField] private Image creatureImage;
    [SerializeField] private TMP_Text labelText;

    // STATE
    private Biome currentHall;

    void Start()
    {
        // Current Hall
        currentHall = UniversalManagers.instance.GetComponentInChildren<StateManager>().CurrentHall;

        // Set Up
        SetBG();
    }

    private void SetBG()
    {
        if (currentHall == Biome.CoralReef)
        {
            BG.sprite = coralReefBG;
        }

        else if (currentHall == Biome.SeagrassBed)
        {
            BG.sprite = seagrassBedBG;
        }

        else if (currentHall == Biome.OpenOcean)
        {
            BG.sprite = openOceanBG;
        }
    }

    public void SetCreatureImage(Sprite img)
    {
        if (img != null)
        {
            creatureImage.color = Color.white;
            creatureImage.sprite = img;
        }
    }

    public void SetLabel(string txt)
    {
        if (labelText != null)
        {
            labelText.text = txt;
        }

        else
        {
            Debug.LogWarning("Photo Item has no label text UI");
        }
    }
}
