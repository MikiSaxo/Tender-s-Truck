using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    [SerializeField] private GameObject _mozzaPaned;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TouchType>() != null)
        {
            LifeManager.Instance.WinLife();
            ScoreManager.Instance.AddPoints(other.GetComponent<TouchType>().Type);
            AudioManager.Instance.PlaySound("TouchMozza");
            Instantiate(_mozzaPaned);
            _mozzaPaned.transform.position = other.gameObject.transform.position;
            Destroy(other.gameObject);
            Destroy(_mozzaPaned, 5);
        }
    }
}