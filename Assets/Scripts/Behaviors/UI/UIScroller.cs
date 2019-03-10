using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Generic UI scroller.
/// </summary>
public class UIScroller : MonoBehaviour
{
    public int startIndex = 0;
    
    [Range(0, 1)]
    public float lerpFactor = 0.1f;

    public KeyCode leftKey;
    public KeyCode rightKey;

    // FIXME: there's no GetButtonDown on InputMgr
    public InputMgr.eButton leftButton;
    public InputMgr.eButton rightButton;

    public HorizontalOrVerticalLayoutGroup layoutGroup;

    public int currentIndex;
    [SerializeField]
    private Vector2 targetPosition;
    private RectTransform rectTransform;
    private float elementWidth;

    public delegate void OnIndexChangeDelegate(int indexFrom);
    public event OnIndexChangeDelegate OnIndexChange;


    private void Awake()
    {
        layoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        elementWidth = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
        currentIndex = startIndex;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransform.sizeDelta.x == 0) // locks interaction until screen is properly set
        {
            return;
        }

        if (InputMgr.GetButton(1,leftButton) || InputMgr.GetButton(1, rightButton))

        
        if (Input.GetKeyDown(leftKey) || InputMgr.GetKeyDown(1,leftButton))
        {
            //move to left element
            if (currentIndex > 0)
            {
                currentIndex--;
                targetPosition = TargetPosition(currentIndex);
                OnIndexChange?.Invoke(currentIndex + 1);
            }
        }
        if (Input.GetKeyDown(rightKey) || InputMgr.GetKeyDown(1,rightButton))
        {
            //move to right element
            if (currentIndex < transform.childCount - 1)
            {
                currentIndex++;
                targetPosition = TargetPosition(currentIndex);
                OnIndexChange?.Invoke(currentIndex - 1);
            }
        }
    }

    private void OnEnable()
    {
        Invoke("TargetPositionToCurrent", Time.fixedDeltaTime);
    }

    private void OnDisable()
    {
        transform.localPosition = targetPosition;
    }

    private void FixedUpdate()
    {
        if ((Vector2)transform.localPosition != targetPosition)
        {
            LerpToTargetPosition(targetPosition);
        }
    }

    private void TargetPositionToCurrent()
    {
        targetPosition = TargetPosition(currentIndex);
    }

    private Vector2 TargetPosition (int targetIndex)
    {
        Vector2 targetPosition = Vector2.zero;
        if (rectTransform.sizeDelta.x != 0)
        {
            targetPosition.x = rectTransform.sizeDelta.x / 2 - (layoutGroup.padding.left + layoutGroup.spacing * targetIndex + elementWidth * (0.5f + currentIndex));
        }
        return targetPosition;
    }

    private void LerpToTargetPosition (Vector2 tgtPosition)
    {
        float distanceX = tgtPosition.x - transform.localPosition.x;
        //Debug.Log("DISTANCE X = " + distanceX);
        transform.localPosition = Vector2.Lerp(transform.localPosition, tgtPosition, lerpFactor);
    }

}

