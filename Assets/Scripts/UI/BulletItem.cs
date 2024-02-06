using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BulletItem : MonoBehaviour
{
    [SerializeField] private TMP_Text bulletText;
    [SerializeField] private Color emphasisColor;

    public void SetText(string txt)
    {
        bulletText.text = txt;
    }

    public void SetEmphasizedText(string txt)
    {
        bulletText.color = emphasisColor;
        bulletText.text = "<b>" + txt + "<b>";
    }
}
