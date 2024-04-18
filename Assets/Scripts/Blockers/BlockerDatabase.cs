using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlockerDatabase : MonoBehaviour
{
    [SerializeField] private BlockerOrder[] allBlockerOrders;

    private List<ParentBlocker> parentBlockers = new List<ParentBlocker>();
    private List<ParentBlocker> capturedBlockers = new List<ParentBlocker>();

    private ParentBlocker[] _allBlockers;
    public ParentBlocker[] AllBlockers => _allBlockers;

    void Awake()
    {
        // Sort blockers according to location order
        allBlockerOrders = allBlockerOrders.OrderBy(b => b.LocationOrder).ToArray();

        // Add sorted blockers to list
        foreach (BlockerOrder blockerOrder in allBlockerOrders)
        {
            parentBlockers.Add(blockerOrder.Blocker);
        }

        // Convert list to array
        _allBlockers = parentBlockers.ToArray();
    }

    public ParentBlocker[] GetCapturedBlockers()
    {
        // Reset
        capturedBlockers.Clear();

        // Add if captured 4 instances
        foreach (ParentBlocker blocker in _allBlockers)
        {
            if (blocker.CaptureCount == 4)
            {
                capturedBlockers.Add(blocker);
            }
        }

        return capturedBlockers.ToArray();
    }
}
