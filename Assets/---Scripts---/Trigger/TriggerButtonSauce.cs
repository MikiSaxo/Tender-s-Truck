using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButtonSauce : MonoBehaviour
{
    [SerializeField] private ElementType _whichType;
    // [SerializeField] private GameObject _sauce;
    [SerializeField] private GameObject[] _sabers;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TriggerDetector>() != null)
        {
            _sabers[0].GetComponent<SliceObject>().ChangeSauceType(_whichType);
            
            var color = 0;
            if (_whichType == ElementType.RedHorizontal || _whichType == ElementType.RedVertical)
                color = 1;
            
            SauceProjection.Instance.ChangeColor(color);
        }
    }

    public void UpdateSauce(ElementType newType)
    {
        _whichType = newType;
    }
}
