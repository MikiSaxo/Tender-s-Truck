using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneElement : MonoBehaviour
{
    public static event Action DeathElement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ElementToSpawn>())
        {
            DeathElement?.Invoke();
        }
    }
}
