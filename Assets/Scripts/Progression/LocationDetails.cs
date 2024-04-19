using UnityEngine;

[System.Serializable]
public class LocationDetails
{
    [SerializeField]
    private int _order;
    public int Order => _order;
    
    [SerializeField]
    private string _code;
    public string Code => _code;

    [SerializeField]
    private LocationBuoy _buoy;
    public LocationBuoy Buoy => _buoy;
}
