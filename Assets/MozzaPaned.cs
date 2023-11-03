using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MozzaPaned : MonoBehaviour
{
    [SerializeField] private float _timeToDie;
    void Start()
    {
        Destroy(gameObject, _timeToDie);
    }
}
