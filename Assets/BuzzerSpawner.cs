using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class BuzzerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _sliceCubePrefab;
    [SerializeField] private Vector3 _spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SphereTestCollision>() != null)
        {
            print("touch");
            GameObject go = Instantiate(_sliceCubePrefab, _spawnPoint, transform.rotation);
        }
    }
}
