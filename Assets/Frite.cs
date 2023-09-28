using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Frite : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private MeshRenderer _meshRenderer;

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

    public WhichType GetFriteType()
    {
        return _currentType;
    }
}
