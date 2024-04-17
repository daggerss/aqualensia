using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blocker", menuName = "Aqualensia/Blockers/Blocker", order = 1)]
public class Blocker : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField]
    private Sprite[] _sprites;
    public Sprite[] Sprites => _sprites;

    [Header("Basic Information")]
    [SerializeField]
    private ParentBlocker _parentBlocker;
    public ParentBlocker ParentBlocker => _parentBlocker;

    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private bool _stationary;
    public bool Stationary => _stationary;

    [Header("Behavior")]
    [SerializeField]
    private float _frequencyScale;
    public float FrequencyScale => _frequencyScale;

    [SerializeField]
    private float _speed;
    public float Speed => _speed;
}
