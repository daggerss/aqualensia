using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Aqualensia/Creature", order = 0)]
public class Creature : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField]
    private Sprite _schoolSprite;
    public Sprite SchoolSprite => _schoolSprite;

    [SerializeField]
    private Sprite _realPhoto;
    public Sprite RealPhoto => _realPhoto;

    [Header("Basic Information")]
    [SerializeField]
    private string _commonName;
    public string CommonName => _commonName;

    [SerializeField]
    private string _scientificName;
    public string ScientificName => _scientificName;

    [SerializeField]
    private ConservationStatus _conservationStatus;
    public ConservationStatus ConservationStatus => _conservationStatus;

    [SerializeField]
    private TimeOfDay _activeTime;
    public TimeOfDay ActiveTime => _activeTime;

    [SerializeField]
    private bool _sessile;
    public bool Sessile => _sessile;

    [SerializeField]
    private OceanZone _upperZone;
    public OceanZone UpperZone => _upperZone;

    [SerializeField]
    private OceanZone _lowerZone;
    public OceanZone LowerZone => _lowerZone;

    [field: Header("Research Information")]
    [SerializeField]
    [TextArea(3, 10)]
    private string[] _photoInfo = new string[4];
    public string[] PhotoInfo => _photoInfo;

    [SerializeField]
    [TextArea(3, 10)]
    private string _galleryInfo;
    public string GalleryInfo => _galleryInfo;

    [Header("Behavior")]
    [SerializeField]
    private float _populationScale;
    public float PopulationScale => _populationScale;

    [SerializeField]
    private float _speed;
    public float Speed => _speed;

    [field: Header("Progression")]
    [field: SerializeField]
    public CreatureStatus CaptureStatus {get; set;}

    [field: SerializeField]
    private int _captureCount;
    public int CaptureCount
    {
        get
        {
            return _captureCount;
        }
        set
        {
            _captureCount = Mathf.Clamp(value, 0, 4);
        }
    }

    [field: SerializeField]
    public bool isBlocked {get; set;}
}
