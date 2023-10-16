using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSauceMesh : MonoBehaviour
{
    enum ColorButton
    {
        Yellow = 0,
        Red = 1
    }

    [SerializeField] private ColorButton _color;
    [SerializeField] private GameObject[] _grosPot;
    [SerializeField] private GameObject[] _boutonPoussoir;
    [SerializeField] private GameObject[] _etiquette;

    private void Start()
    {
        InitSauce();
    }

    private void InitSauce()
    {
        for (int i = 0; i < _grosPot.Length; i++)
        {
            _grosPot[i].SetActive(false);
            _boutonPoussoir[i].SetActive(false);
            _etiquette[i].SetActive(false);
        }

        _grosPot[(int)_color].SetActive(true);
        _boutonPoussoir[(int)_color].SetActive(true);
        _etiquette[(int)_color].SetActive(true);
    }
}