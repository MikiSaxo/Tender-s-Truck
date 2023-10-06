using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardSign : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private BoardEditor _boardEditor;
    [SerializeField] private BoardPosition _boardPosition;
    [FormerlySerializedAs("_fritePrefab")] [SerializeField] private GameObject _elementPrefab;
    [Header("Board Sign")]
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material[] _materials;

    private GameObject _stockElement;
    private ElementType _elementType;

    private void Start()
    {
        _elementType = ElementType.Nothing;
    }

    private void OnMouseEnter()
    {
        _renderer.material = _materials[0];
    }

    private void OnMouseExit()
    {
        _renderer.material = _materials[1];
    }

    private void OnMouseDown()
    {
        if(_stockElement != null)
            Destroy(_stockElement);

        if (EditorManager.Instance.GetCurrentElement() != ElementType.Nothing)
        {
            AddElement();
            SaveElement();
        }
        else
        {
            if (_elementType == ElementType.Nothing)
                return;
            
            _boardEditor.RemoveElementToSave(_boardPosition, _elementType);
            _elementType = ElementType.Nothing;
        }
    }

    public void AddElement()
    {
        GameObject element = Instantiate(_elementPrefab);
        _stockElement = element;

        _elementType = EditorManager.Instance.GetCurrentElement();
        element.transform.position = transform.position;
        element.transform.DOScale(element.transform.localScale * 2, 0);
        element.GetComponent<Element>().Init(_elementType, true);
        element.transform.SetParent(transform);
            
    }

    private void SaveElement()
    {
        _boardEditor.AddElementToSave(_boardPosition, _elementType);
    }
}

public enum BoardPosition
{
    LeftTop = 0,
    LeftDown = 1,
    MidTop = 2,
    MidDown = 3,
    RightTop = 4,
    RightDown = 5,
}