using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Point>() != null)
        {
            print("touche le loup");
            Destroy(other.gameObject);
        }
    }
}
