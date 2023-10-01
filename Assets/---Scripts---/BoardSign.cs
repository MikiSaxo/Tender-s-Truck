using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class BoardSign : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private BoardEditor _boardEditor;
    [SerializeField] private BoardPosition _boardPosition;
    [SerializeField] private GameObject _fritePrefab;
    [Header("Board Sign")]
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material[] _materials;

    private GameObject _stockFrite;
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
        if(_stockFrite != null)
            Destroy(_stockFrite);

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
        GameObject frite = Instantiate(_fritePrefab);
        _stockFrite = frite;

        _elementType = EditorManager.Instance.GetCurrentElement();
        frite.transform.position = transform.position;
        frite.transform.DOScale(frite.transform.localScale * 2, 0);
        frite.GetComponent<Frite>().Init(_elementType, true);
        frite.GetComponent<Rigidbody>().isKinematic = true;
        frite.transform.SetParent(transform);
            
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