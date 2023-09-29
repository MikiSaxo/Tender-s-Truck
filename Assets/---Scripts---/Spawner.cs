using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _fritePrefab;
    [SerializeField] private Vector3 _impulseDir;
    [SerializeField] private float _force;


    public void SpawnRedHorizontal()
    {
        SpawnFrite(Direction.Horizontal, WhichType.Red);
    }
    public void SpawnRedVertical()
    {
        SpawnFrite(Direction.Vertical, WhichType.Red);
    }
    public void SpawnYellowHorizontal()
    {
        SpawnFrite(Direction.Horizontal, WhichType.Yellow);
    }
    public void SpawnYellowVertical()
    {
        SpawnFrite(Direction.Vertical, WhichType.Yellow);
    }
    private void SpawnFrite(Direction side, WhichType type)
    {
        GameObject go = Instantiate(_fritePrefab, transform);

        // var rb = go.GetComponent<Rigidbody>();
        // rb.AddForce(_impulseDir * _force);


        go.GetComponent<Frite>().Init(side, type);

        Destroy(go, 10);
    }
}

public enum Direction
{
    Horizontal = 0,
    Vertical = 1,
}
