using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineDownBar : MonoBehaviour
{
    RectTransform _elementRectTransform;

    private void Start()
    {
        _elementRectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void MoveBoards()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_elementRectTransform, mousePosition, null, out localMousePosition);

        float positionXRelativeToElement = localMousePosition.x;

        float normalizedPositionX = Mathf.Clamp01((positionXRelativeToElement + _elementRectTransform.rect.width / 2) / _elementRectTransform.rect.width) * 100f;
        
        BoardManager.Instance.MoveBoards(normalizedPositionX);
    }
}
