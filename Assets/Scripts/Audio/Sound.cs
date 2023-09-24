using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private AudioClip _clip;
    public AudioClip Clip => _clip;

    [SerializeField]
    private bool _loop;
    public bool Loop => _loop; 

    [SerializeField]
    [Range(0f,1f)]
    private float _volume;
    public float Volume
    {
        get { return _volume; }
        set { _volume = value; }
    }

    [SerializeField]
    [Range(.1f,3f)]
    private float _pitch;
    public float Pitch
    {
        get { return _pitch; }
        set { _pitch = value; }
    }
}
