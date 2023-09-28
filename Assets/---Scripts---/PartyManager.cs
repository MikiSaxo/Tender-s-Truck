using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance;

    [SerializeField] private Material[] _friteTypes;

    private void Awake()
    {
        Instance = this;
    }

    public Material GetFriteType(int index)
    {
        return _friteTypes[index];
    }
}
