using UnityEngine;

[System.Serializable]
public class DialogueSequence
{
    [SerializeField]
    private int _sequenceID;
    public int SequenceID => _sequenceID;

    [SerializeField]
    private Dialogue[] _dialogues;
    public Dialogue[] Dialogues => _dialogues;
}
