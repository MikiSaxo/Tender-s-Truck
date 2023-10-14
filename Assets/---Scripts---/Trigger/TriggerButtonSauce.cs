using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButtonSauce : MonoBehaviour
{
    [SerializeField] private ElementType _whichType;
    [SerializeField] private GameObject _sauce;
    [SerializeField] private GameObject[] _sabers;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TriggerDetector>() != null)
        {
            // _sauce.GetComponent<BacASauce>().ChangeSauce(_whichType);
            _sabers[0].GetComponent<SliceObject>().ChangeSauceType(_whichType);
            _sabers[1].GetComponent<SliceObject>().ChangeSauceType(_whichType);
        }
    }
}
