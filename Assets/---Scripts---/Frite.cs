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

    private WhichType _currentType;

    private void Start()
    {
        if (_meshes.Length > 0)
        {
            int randomNumber = Random.Range(0, _meshes.Length);
            _friteMesh.mesh = _meshes[randomNumber];
        }
    }

    public void Init(Direction side, WhichType initType)
    {
        if (side == Direction.Vertical)
        {
            transform.DORotate(new Vector3(0, 0, -90), 0);
        }

        _currentType = initType;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = PartyManager.Instance.GetFriteType((int)_currentType);
    }

    private void Update()
    {
        transform.Translate(_direction, Space.World);
    }

    public WhichType GetFriteType()
    {
        return _currentType;
    }
}
