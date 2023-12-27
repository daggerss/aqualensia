using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class LogCapturedUI
{
    [SerializeField] private Image creatureImage;
    [SerializeField] private GameObject shots;
    [SerializeField] private TMP_Text shotsText;
    [SerializeField] private GameObject tag;

    public void SetImage(Sprite img)
    {
        creatureImage.sprite = img;
    }

    public void SetShots(int shotsNum)
    {
        shots.SetActive(true);
        shotsText.text = shotsNum.ToString();
    }

    public void SetTag(bool flag)
    {
        tag.SetActive(flag);
    }

    public void Reset()
    {
        creatureImage.sprite = null;
        creatureImage.color = Color.clear;
        shots.SetActive(false);
        tag.SetActive(false);
    }
}
