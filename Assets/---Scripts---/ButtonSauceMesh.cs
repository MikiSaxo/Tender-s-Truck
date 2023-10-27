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
    [SerializeField] private TriggerButtonSauce _trigger;
    [Header("Light")]
    [SerializeField] private Light _light;
    [SerializeField] private Color[] _colors;
    [SerializeField] private MeshRenderer _lamp;
    [SerializeField] private Material[] _lampMat;

    private bool _hasInit;
    
    private void Start()
    {
        ChangeSauce();
    }

    private void ChangeSauce()
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

        _trigger.UpdateSauce(_color == ColorButton.Red ? ElementType.RedHorizontal : ElementType.YellowHorizontal);

        _light.color = _colors[(int)_color];
        _lamp.material = _lampMat[(int)_color];
    }

    public void UpdateSauce()
    {
        _color = _color == ColorButton.Red ? ColorButton.Yellow : ColorButton.Red;
        ChangeSauce();
    }
}