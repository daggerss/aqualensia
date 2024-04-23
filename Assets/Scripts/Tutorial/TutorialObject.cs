using UnityEngine;

[System.Serializable]
public class TutorialObject
{
    [SerializeField]
    private int _dialogueIndex;
    public int DialogueIndex => _dialogueIndex;

    [SerializeField]
    private TutorialAction _action;
    public TutorialAction Action => _action;

    [SerializeField]
    private GameObject[] _gameObjects;
    public GameObject[] GameObjects => _gameObjects;
}
