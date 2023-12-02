using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Aqualensia/Creature", order = 0)]
public class Creature : ScriptableObject
{
    [field: SerializeField]
    public Sprite Sprite {get; private set;}

    [Header("Basic Information")]
    [SerializeField]
    private string _commonName;
    public string CommonName => _commonName;

    [SerializeField]
    private string _scientificName;
    public string scientificName => _scientificName;
}
