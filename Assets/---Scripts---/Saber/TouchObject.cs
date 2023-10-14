using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TouchType>() != null)
        {
            print("touche le loup");
            LifeManager.Instance.WinLife();
            ScoreManager.Instance.AddPoints(other.GetComponent<TouchType>().Type);
            Destroy(other.gameObject);
        }
    }
}