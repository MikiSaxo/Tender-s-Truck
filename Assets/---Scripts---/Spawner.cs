using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _fritePrefab;
    [SerializeField] private Vector3 _impulseDir;
    [SerializeField] private float _force;


    private void SpawnFrite(int index)
    {
        GameObject go = Instantiate(_fritePrefab);
        go.transform.position = transform.position;
        
        // var rb = go.GetComponent<Rigidbody>();
        // rb.AddForce(_impulseDir * _force);


        go.GetComponent<Frite>().Init((ElementType)index, false);

        Destroy(go, 10);
    }
}