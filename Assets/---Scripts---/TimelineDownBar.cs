using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TimelineDownBar : MonoBehaviour
{
    public static TimelineDownBar Instance;
    
    [SerializeField] private GameObject _cursorTimeline;
    
    private RectTransform _elementRectTransform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _elementRectTransform = gameObject.GetComponent<RectTransform>();
        MoveCursor(0);
    }

    public void MoveBoards()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_elementRectTransform, mousePosition, null, out localMousePosition);

        float positionXRelativeToElement = localMousePosition.x;

        
        float normalizedPositionX = Mathf.Clamp01((positionXRelativeToElement + _elementRectTransform.rect.width / 2) / _elementRectTransform.rect.width) * 100f;
        
        BoardManager.Instance.MoveBoards(normalizedPositionX);
        MoveCursor(normalizedPositionX);
    }

    public void MoveCursor(float value)
    {
        // float test = Mathf.Clamp(value, -_elementRectTransform.rect.width*.5f, _elementRectTransform.rect.width*.5f);

        float halfSize = _elementRectTransform.rect.width * .5f;
        float getDistance = -halfSize + (halfSize-(-halfSize))  * value / 100;
        
        _cursorTimeline.transform.localPosition = new Vector2(getDistance, _cursorTimeline.transform.localPosition.y);

    }
}
