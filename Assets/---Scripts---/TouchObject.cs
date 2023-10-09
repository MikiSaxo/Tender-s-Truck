using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Croquette>() != null)
        {
            print("touche le loup");
            LifeManager.Instance.WinLife();
            ScoreManager.Instance.AddPoints(other.GetComponent<Croquette>().Type);
            Destroy(other.gameObject);
        }
    }
}