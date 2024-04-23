using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TutorialTrigger
{
    [SerializeField]
    private int _sequenceID;
    public int SequenceID => _sequenceID;

    [SerializeField]
    private int _dialogueIndex;
    public int DialogueIndex => _dialogueIndex;

    [SerializeField]
    private Button _button;
    public Button Button => _button;
}
