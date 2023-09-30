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
    public Dictionary<int, BoardPosition> NbOfElementByPosition = new Dictionary<int, BoardPosition>();
    public List<ElementType> ElementTypes;
}
