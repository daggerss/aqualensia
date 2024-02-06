using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [Header("UI")]
    [SerializeField] private RectTransform contentRect;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;

    [Tooltip("Page Width + Spacing")]
    [SerializeField] private Vector3 pageStep;

    [Header("Scroll")]
    [SerializeField] private Cooldown scrollCooldown;

    [Header("Tween")]
    [SerializeField] private float tweenTime;
    [SerializeField] private LeanTweenType tweenType;

    [Header("Page Indicator")]
    [SerializeField] private Transform indicatorParent;
    [SerializeField] private GameObject indicatorPrefab;

    // BASIC
    private Vector3 startingPos;

    private int currentPage = 1;
    public int CurrentPage
    {
        get
        {
            return currentPage;
        }

        set
        {
            // Clamp to max children
            currentPage = Mathf.Clamp(value, 1, contentRect.childCount);
        }
    }

    private Vector3 targetPos;
    public Vector3 TargetPos
    {
        get
        {
            return targetPos;
        }

        set
        {
            // Clamp x - assumes left to right movement
            targetPos.x = Mathf.Clamp(value.x, pageStep.x * (contentRect.childCount - 1), startingPos.x);
        }
    }

    // INDICATOR
    private List<GameObject> pageIndicators = new List<GameObject>();
    private GameObject currentIndicator;

    void Awake()
    {
        // Save starting position
        startingPos = contentRect.localPosition;

        // Set initial position
        targetPos = contentRect.localPosition;
    }

    void Start()
    {
        // Show/Hide arrows
        DisplayArrows();

        // Page indicator
        if (indicatorParent != null)
        {
            SetUpIndicator();
            UpdateIndicator();
        }
    }

    /* --------------------------- Basic Functions -------------------------- */
    public void Next()
    {
        if (CurrentPage < contentRect.childCount)
        {
            CurrentPage++;
            TargetPos += pageStep;
            MovePage();
        }
    }

    public void Prev()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            TargetPos -= pageStep;
            MovePage();
        }
    }

    public void Jump(int pageNum)
    {
        // Clamp page num
        pageNum = Mathf.Clamp(pageNum, 1, contentRect.childCount);
        
        // Next
        if (CurrentPage < pageNum)
        {
            TargetPos += pageStep * (pageNum - CurrentPage);
        }

        // Prev
        else if (CurrentPage > pageNum)
        {
            TargetPos -= pageStep * (CurrentPage - pageNum);
        }

        // Update current page
        CurrentPage = pageNum;

        MovePage();
    }

    // Move by step
    public virtual void MovePage()
    {
        contentRect.LeanMoveLocal(TargetPos, tweenTime).setEase(tweenType);

        // Show/Hide arrows
        DisplayArrows();

        // Indicator
        if (indicatorParent != null)
        {
            UpdateIndicator();
        }
    }

    /* ------------------------------- Arrows ------------------------------- */
    // Toggle arrows
    private void DisplayArrows()
    {
        if ((leftArrow != null) && (rightArrow != null))
        {
            ShowArrow((CurrentPage != 1), leftArrow);
            ShowArrow((CurrentPage != contentRect.childCount), rightArrow);
        }
    }

    // Toggle arrow
    private void ShowArrow(bool shouldShow, GameObject arrow)
    {
        arrow.SetActive(shouldShow);
    }

    /* ----------------------------- Swipe Snap ----------------------------- */
    // Swipe snap - scroll
    // Note: Uses cooldown to force step interval
    public void ScrollSnap()
    {
        if (!scrollCooldown.IsCoolingDown && TryGetComponent<ScrollRect>(out ScrollRect scrollRect))
        {
            scrollCooldown.StartCooldown();

            // Forward
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                Prev();
            }
    
            // Backward
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                Next();
            }
        }
    }

    // Swipe snap - drag
    // ! Note: To work, this should be attached to the ScrollRect space
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.position.x > eventData.pressPosition.x)
        {
            Prev();
        }

        else
        {
            Next();
        }
    }

    /* --------------------------- Page Indicator --------------------------- */
    // Create indicator dots
    private void SetUpIndicator()
    {
        for (int i = 0; i < contentRect.childCount; i++)
        {
            pageIndicators.Add(Instantiate(indicatorPrefab, indicatorParent));
        }
    }

    // Update dots
    private void UpdateIndicator()
    {
        // Reset
        foreach (GameObject dot in pageIndicators)
        {
            if (dot.TryGetComponent<Image>(out Image img))
            {
                img.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
        }

        // Set active
        currentIndicator = pageIndicators[CurrentPage - 1];
        if (currentIndicator.TryGetComponent<Image>(out Image currentImage))
        {
            currentImage.color = Color.white;
        }
    }
}
