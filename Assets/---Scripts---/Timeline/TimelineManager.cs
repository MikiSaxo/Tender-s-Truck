using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private GameObject _smallBarPref;
    [SerializeField] private GameObject _gridParent;
    void Start()
    {
        for (int i = 0; i < 120; i++)
        {
            GameObject go = Instantiate(_smallBarPref, _gridParent.transform);
            go.GetComponent<TimelineSmallBar>().Init(i);
        }
    }
}
