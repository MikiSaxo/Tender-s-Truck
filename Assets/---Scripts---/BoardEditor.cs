using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardEditor : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberBoardText;
    [SerializeField] private TMP_Text _numberBPMText;
    
    private int _numberBoard;

    public void Init(int numberBoard, int numberBPM)
    {
        _numberBoardText.text = $"n°{numberBoard}";
        _numberBPMText.text = $"{++numberBPM}/4";
        _numberBoard = numberBoard;
    }

    public void AddElementToSave(BoardPosition boardPos, ElementType elementType)
    {
        EditorSaveMap.Instance.AddElement(_numberBoard, boardPos, elementType);
    }
    public void RemoveElementToSave(BoardPosition boardPos, ElementType elementType)
    {
        EditorSaveMap.Instance.RemoveElement(_numberBoard, boardPos, elementType);
    }
}