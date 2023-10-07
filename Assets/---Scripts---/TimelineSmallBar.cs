using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineSmallBar : MonoBehaviour
{
    private int _index;
    
    public void Init(int index)
    {
        _index = index;
    }

    public void MoveBoardManager()
    {
        BoardManager.Instance.MoveBoards(_index);
    }
}
