using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class LogCapturedUI : MonoBehaviour
{
    [SerializeField] private Image creatureImage;
    [SerializeField] private GameObject shots;
    [SerializeField] private TMP_Text shotsText;
    [SerializeField] private GameObject newTag;

    public void SetImage(Sprite img)
    {
        creatureImage.sprite = img;
        creatureImage.color = Color.white;
    }

    public void SetShots(int shotsNum)
    {
        shots.SetActive(true);
        shotsText.text = shotsNum.ToString();
    }

    public void SetTag(bool flag)
    {
        newTag.SetActive(flag);
    }

    public void Reset()
    {
        creatureImage.sprite = null;
        creatureImage.color = Color.clear;
        shots.SetActive(false);
        newTag.SetActive(false);
    }
}
