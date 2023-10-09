using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneElement : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ElementToSpawn>())
        {
            other.GetComponent<ElementToSpawn>().OnDeathElementTrigger();
        }
    }
}
