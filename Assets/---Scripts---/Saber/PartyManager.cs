using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance;

    [SerializeField] private Material[] _elementTypes;

    private void Awake()
    {
        Instance = this;
    }

    public Material GetElementTypeMat(int index)
    {
        return _elementTypes[index];
    }
}
