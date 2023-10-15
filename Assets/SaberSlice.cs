using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberSlice : MonoBehaviour
{
    [SerializeField] private SliceObject _parent;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Frite>())
        {
            var friteType = other.gameObject.GetComponent<ElementChild>().CurrentType;
            // print("ça touche les frites : " + friteType);
            _parent.TrySlice(friteType, other.gameObject);
        }
    }
}   
