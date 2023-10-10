using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLaunchMap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TriggerDetector>() != null)
        {
            Spawner.Instance.LaunchMap();
        }
    }
}
