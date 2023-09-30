using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    public static EditorManager Instance;

    [SerializeField] private ElementType _currentElement;
    [SerializeField] private GameObject _fritePrefab;
    [SerializeField] private Material[] _friteMaterials;
    [SerializeField] private GameObject[] _leftButtonsHighlight;

    private int _currentBtnHglt;

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
        _leftButtonsHighlight[_currentBtnHglt].SetActive(false);
        _currentBtnHglt = index;
        _leftButtonsHighlight[_currentBtnHglt].SetActive(true);
    }

    public ElementType GetCurrentElement()
    {
        return _currentElement;
    }

    public GameObject GetElementToInstantiate()
    {
        return _fritePrefab;
    }

    public Material GetFriteType(int index)
    {
        return _friteMaterials[index];
    }
}

public enum ElementType
{
    YellowHorizontal = 0,
    YellowVertical = 1,
    RedHorizontal = 2,
    RedVertical = 3,
    Nothing = 4
}