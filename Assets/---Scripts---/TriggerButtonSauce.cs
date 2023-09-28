using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButtonSauce : MonoBehaviour
{
    [SerializeField] private WhichType _whichType;
    [SerializeField] private GameObject _sauce;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TriggerDetector>() != null)
        {
            _sauce.GetComponent<BacASauce>().ChangeSauce(_whichType);
        }
    }
}

public enum WhichType
{
    Yellow = 0,
    Red = 1
}
