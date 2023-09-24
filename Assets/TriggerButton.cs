using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : MonoBehaviour
{
    [SerializeField] private GameObject _sliceCubePrefab;
    [SerializeField] private Vector3 _spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<SphereTestCollision>() != null)
        {
            GameObject go = Instantiate(_sliceCubePrefab, _spawnPoint, transform.rotation);
        }
    }
}
