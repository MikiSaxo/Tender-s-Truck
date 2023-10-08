using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PartyManager : MonoBehaviour
{
    public static PartyManager Instance;
    public WhichHanded WhichHanded => _whichHanded;

    [Header("Choose Hand")]
    [SerializeField] private WhichHanded _whichHanded;
    [SerializeField] private GameObject[] _leftHand;
    [SerializeField] private GameObject[] _rightHand;
    [Header("Materials")]
    [SerializeField] private Material[] _elementTypes;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_whichHanded == WhichHanded.Left)
        {
            _leftHand[0].SetActive(true);
           _leftHand[1].SetActive(false);
           _rightHand[0].SetActive(false);
           _rightHand[1].SetActive(true);
        }
        else
        {
           _leftHand[0].SetActive(false);
           _leftHand[1].SetActive(true);
           _rightHand[0].SetActive(true);
           _rightHand[1].SetActive(false);
        }
    }

    public Material GetElementTypeMat(int index)
    {
        return _elementTypes[index];
    }
}

public enum WhichHanded
{
    Right = 0,
    Left = 1
}
