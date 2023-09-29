using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardEditor : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberBoard;
    [SerializeField] private TMP_Text _numberBPM;

    public void Init(int numberBoard, int numberBPM)
    {
        _numberBoard.text = $"n°{numberBoard}";
        _numberBPM.text = $"1/{numberBPM}";
    }
}
