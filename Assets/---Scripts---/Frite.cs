using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Frite : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Vector3 _direction;

    private WhichType _currentType;

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
