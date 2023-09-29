using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoardSign : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material[] _materials;
    [SerializeField] private GameObject _fritePrefab;

    private GameObject _stockFrite;

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
            GameObject frite = Instantiate(_fritePrefab);
            _stockFrite = frite;

            frite.transform.position = transform.position;
            frite.transform.DOScale(frite.transform.localScale * 2, 0);
            frite.GetComponent<Frite>().Init(EditorManager.Instance.GetCurrentElement(), true);
            frite.GetComponent<Rigidbody>().isKinematic = true;
            frite.transform.SetParent(transform);
        }
    }
}