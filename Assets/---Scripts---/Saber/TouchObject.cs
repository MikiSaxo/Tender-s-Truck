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
            LifeManager.Instance.WinLife();
            ScoreManager.Instance.AddPoints(other.GetComponent<TouchType>().Type);
            AudioManager.Instance.PlaySound("TouchMozza");
            Destroy(other.gameObject);
        }
    }
}