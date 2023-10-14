using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EditorManager : MonoBehaviour
{
    public static EditorManager Instance;

    [SerializeField] private ElementType _currentElement;
    [SerializeField] private GameObject _fritePrefab;
    [SerializeField] private Material[] _ElementMaterials;
    [SerializeField] private GameObject[] _leftButtonsHighlight;

    private int _currentBtnHighlight;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ChangeElement(4);
        }
    }

    public void ChangeElement(int index)
    {
        _currentElement = (ElementType)index;
        ClickOnBtnHighlight(index);
    }

    public void ClickOnBtnHighlight(int index)
    {
        _leftButtonsHighlight[_currentBtnHighlight].SetActive(false);
        _currentBtnHighlight = index;
        _leftButtonsHighlight[_currentBtnHighlight].SetActive(true);
    }

    public ElementType GetCurrentElement()
    {
        return _currentElement;
    }

    public GameObject GetElementToInstantiate()
    {
        return _fritePrefab;
    }

    public Material GetElementType(int index)
    {
        return _ElementMaterials[index];
    }
}

public enum ElementType
{
    YellowHorizontal = 0,
    YellowVertical = 1,
    RedHorizontal = 2,
    RedVertical = 3,
    Nothing = 4,
    Croquette = 5,
    Mozza_ClockWise = 6,
    Mozza_Anti_ClockWise = 7
}