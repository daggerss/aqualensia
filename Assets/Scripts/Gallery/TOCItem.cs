using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TOCItem : MonoBehaviour, IPointerClickHandler
{
    private HallManager hallManager;
    private int pageNumber;

    public void SetUp(HallManager HM, int idx)
    {
        hallManager = HM;
        pageNumber = idx + 1; // page numbers start at 1
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (hallManager != null)
        {
            hallManager.SelectForTOC(pageNumber);
        }
    }
}
