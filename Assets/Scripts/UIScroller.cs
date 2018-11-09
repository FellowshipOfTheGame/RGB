using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScroller : MonoBehaviour
{
    public int startIndex = 0;
    public int elementWidth = 900;
    [Range(0, 1)]
    public float lerpFactor = 0.1f;

    public KeyCode leftKey;
    public KeyCode rightKey;

    public HorizontalOrVerticalLayoutGroup layoutGroup;

    public int currentIndex;
    [SerializeField]
    private Vector2 targetPosition;
    private RectTransform rectTransform;

    private float centralIndex;

    private void Awake()
    {
        layoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = startIndex;
        centralIndex = (transform.childCount - 1) / 2;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(leftKey))
        {
            //move to left element
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex++;
            }
            targetPosition = TargetPosition(currentIndex);
        }
        if (Input.GetKeyDown(rightKey))
        {
            //move to right element
            currentIndex++;
            if (currentIndex >= transform.childCount)
            {
                currentIndex--;
            }
            targetPosition = TargetPosition(currentIndex);
        }
    }

    private void FixedUpdate()
    {
        LerpToTargetPosition(targetPosition);
    }

    private Vector2 TargetPosition (int targetIndex)
    {
        Vector2 targetPosition = Vector2.zero;
        targetPosition.x = rectTransform.sizeDelta.x/2 - (layoutGroup.padding.left + layoutGroup.spacing * targetIndex + elementWidth * (0.5f + currentIndex) );
        return targetPosition;
    }

    private void LerpToTargetPosition (Vector2 tgtPosition)
    {
        float distanceX = tgtPosition.x - transform.localPosition.x;
        transform.localPosition = Vector2.Lerp(transform.localPosition, tgtPosition, lerpFactor);
    }
}
