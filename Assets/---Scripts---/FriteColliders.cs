using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FriteColliders : MonoBehaviour
{
    [SerializeField] private Collider _colliderToDestroy;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SauceStick>())
        {
            _colliderToDestroy.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SauceStick>())
        {
            _colliderToDestroy.enabled = true;
        }
    }
}
