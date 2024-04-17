using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TOCItem : MonoBehaviour, IPointerClickHandler
{
    private HallManager hallManager;
    private ProtestHallManager protestHallManager;
    private int pageNumber;

    // TOC for Creatures
    public void SetUp(HallManager HM, int idx)
    {
        hallManager = HM;
        pageNumber = idx + 1; // page numbers start at 1
    }

    // TOC for Blockers
    public void SetUp(ProtestHallManager HM, int idx)
    {
        protestHallManager = HM;
        pageNumber = idx + 1; // page numbers start at 1
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (hallManager != null)
        {
            hallManager.SelectForTOC(pageNumber);
        }

        else if (protestHallManager != null)
        {
            protestHallManager.SelectForTOC(pageNumber);
        }
    }
}
