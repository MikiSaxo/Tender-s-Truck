using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Frite : MonoBehaviour
{
    [SerializeField] private MeshFilter _friteMesh;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Mesh[] _meshes;
    [SerializeField] private Vector3 _direction;

    private ElementType _currentType;
    private bool _isEditor;

    private void Start()
    {
        if (_meshes.Length > 0)
        {
            int randomNumber = Random.Range(0, _meshes.Length);
            _friteMesh.mesh = _meshes[randomNumber];
        }
    }

    public void Init(ElementType elementToSpawn, bool isEditor)
    {
        _isEditor = isEditor;
        if (elementToSpawn is ElementType.RedVertical or ElementType.YellowVertical)
        {
            transform.DORotate(new Vector3(0, 0, -90), 0);
        }

        _currentType = elementToSpawn;
        _meshRenderer = GetComponent<MeshRenderer>();
        
        if(PartyManager.Instance != null)
            _meshRenderer.material = PartyManager.Instance.GetFriteType((int)_currentType);
        else if(EditorManager.Instance != null)
            _meshRenderer.material = EditorManager.Instance.GetElementType((int)_currentType);
    }

    private void Update()
    {
        if(!_isEditor)
            transform.Translate(_direction, Space.World);
    }

    public ElementType GetFriteType()
    {
        return _currentType;
    }
}
