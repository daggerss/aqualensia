using UnityEngine;

[System.Serializable]
public class TutorialObjSequence
{
    [SerializeField]
    private int _sequenceID;
    public int SequenceID => _sequenceID;

    [SerializeField]
    private TutorialObject[] _tutorialObjects;
    public TutorialObject[] TutorialObjects => _tutorialObjects;
}
