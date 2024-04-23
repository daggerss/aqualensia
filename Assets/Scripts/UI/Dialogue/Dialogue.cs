using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField]
    [TextArea(3, 10)]
    private string _text;
    public string Text => _text;

    [SerializeField]
    private bool _disableTrigger;
    public bool DisableTrigger => _disableTrigger;

    [SerializeField]
    private bool _anchorUp;
    public bool AnchorUp => _anchorUp;
}
