using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [FormerlySerializedAs("_fritePrefab")] [SerializeField] private GameObject _elementPrefab;


    private void SpawnElement(int index)
    {
        GameObject go = Instantiate(_elementPrefab);
        go.transform.position = transform.position;
        
        // var rb = go.GetComponent<Rigidbody>();
        // rb.AddForce(_impulseDir * _force);


        go.GetComponent<ElementToSpawn>().Init((ElementType)index, false);

        Destroy(go, 10);
    }
}