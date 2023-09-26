using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _fritePrefab;
    [SerializeField] private Vector3 _impulseDir;
    [SerializeField] private float _force;


    public void SpawnHorizontalFrite()
    {
        SpawnFrite(Direction.Horizontal);
    }
    public void SpawnVerticalFrite()
    {
        SpawnFrite(Direction.Vertical);
    }
    private void SpawnFrite(Direction side)
    {
        GameObject go = Instantiate(_fritePrefab, transform);

        var rb = go.GetComponent<Rigidbody>();
        rb.AddForce(_impulseDir * _force);

        if(side == Direction.Vertical)
        {
            go.transform.DORotate(new Vector3(0, 0, -90), 0);
        }

        Destroy(go, 10);
    }
}

public enum Direction
{
    Horizontal = 0,
    Vertical = 1,
}
