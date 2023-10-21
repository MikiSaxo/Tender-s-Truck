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
    private float _halfSize;
    private double _currentValue;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _elementRectTransform = gameObject.GetComponent<RectTransform>();
        _halfSize = _elementRectTransform.rect.width * .5f;
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

    // Value between 0 and 100
    public void MoveCursor(double value)
    {
        // float test = Mathf.Clamp(value, -_elementRectTransform.rect.width*.5f, _elementRectTransform.rect.width*.5f);

        double getDistance = -_halfSize + (_halfSize-(-_halfSize))  * value / 100;
        _currentValue = value;

        _cursorTimeline.transform.localPosition = new Vector2((float)getDistance, _cursorTimeline.transform.localPosition.y);
    }

    public double GetCursorValue()
    {
        return _currentValue;
    }
}
