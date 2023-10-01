using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapConstructData
{
    [Header("Music Infos")]
    public string MusicName;
    public int MusicBPM;

    [Header("Elements Infos")]
    // public Dictionary<List<int>, List<BoardPosType>> ElementByPosIndex = new Dictionary<List<int>, List<BoardPosType>>();
    public List<int> ElementsIndex;
    public List<BoardPosition> ElementsPosition;
    public List<ElementType> ElementsType;
}

public class BoardPosType
{
    public BoardPosition BoardPosition { get; set; }
    public ElementType ElementType { get; set; }
}
