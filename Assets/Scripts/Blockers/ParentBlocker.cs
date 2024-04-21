using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Parent Blocker", menuName = "Aqualensia/Blockers/Parent Blocker", order = 0)]
public class ParentBlocker : ScriptableObject
{
    [Header("Sprites")]
    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [Header("Basic Information")]
    [SerializeField]
    private BlockerType _blockerType;
    public BlockerType BlockerType => _blockerType;

    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private Biome _biome;
    public Biome Biome => _biome;

    [SerializeField]
    private string _location;
    public string Location => _location;

    [SerializeField]
    [TextArea(3, 10)]
    private string _galleryInfo;
    public string GalleryInfo => _galleryInfo;

    [field: Header("Progression")]
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

    [SerializeField]
    private string _price;
    public string Price => _price;

    [SerializeField]
    [TextArea(3, 10)]
    private string _shopInfo;
    public string ShopInfo => _shopInfo;

    [SerializeField]
    [TextArea(3, 10)]
    private string _headline;
    public string Headline => _headline;
}
