using UnityEngine;

[System.Serializable]
public class BlockerOrder
{
    [SerializeField]
    private int _locationOrder;
    public int LocationOrder => _locationOrder;

    [SerializeField]
    private ParentBlocker _blocker;
    public ParentBlocker Blocker => _blocker;
}
