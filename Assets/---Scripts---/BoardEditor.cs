using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardEditor : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberBoardText;
    [SerializeField] private TMP_Text _numberBPMText;
    [SerializeField] private BoardSign[] _boardSigns;
    
    private int _numberBoard;

    public void Init(int numberBoard, int numberBPM)
    {
        if(numberBoard % 2 == 0)
            _numberBoardText.text = $"BPM {numberBoard*.5f}";
        _numberBPMText.text = $"{++numberBPM}/2";
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

    public void OnDestroyElement()
    {
        EditorSaveMap.Instance.DeleteElement(_numberBoard);
    }

    public BoardSign GetBoardSign(int index)
    {
        return _boardSigns[index];
    }
}