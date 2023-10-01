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
    public Dictionary<int, BoardPosType> ElementByPosIndex = new Dictionary<int, BoardPosType>();
}

public class BoardPosType
{
    public BoardPosition BoardPosition { get; set; }
    public ElementType ElementType { get; set; }
}
