using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum ClockWise
{
    Clockwise = 0,
    Anti_Clockwise = 1
}

public class MozzaStick : MonoBehaviour
{
    [SerializeField] private GameObject _mozzaPoint;
    [SerializeField] private int numberOfPoints = 8;
    [SerializeField] private float radius = 1.0f;
    [SerializeField] private float _depth = 1.0f;
    [SerializeField] private ClockWise _sideClockWise;

    private List<GameObject> _points = new List<GameObject>();
    private int _side = -1;

    void Start()
    {
        _points.Add(new GameObject("empty"));

        // if (EditorManager.Instance != null)
            // _depth *= 4;
        
        SpawnPoints();
    }

    public void SpawnClock(ClockWise side)
    {
        _sideClockWise = side;
        SpawnPoints();
    }

    public void SpawnPoints()
    {
        ResetPoints();

        if (_sideClockWise == ClockWise.Anti_Clockwise)
            _side = 1;
        else
            _side = -1;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * 2 * Mathf.PI / numberOfPoints;
            float x = Mathf.Cos(angle) * radius * _side;
            float y = Mathf.Sin(angle) * radius;

            GameObject point = Instantiate(_mozzaPoint, transform);
            point.transform.position = new Vector3(transform.position.x + x, transform.position.y + y,
                transform.position.z + i * 1f / numberOfPoints * _depth);
            _points.Add(point);
        }
    }

    public void ResetPoints()
    {
        if (_points.Count <= 0)
            return;

        foreach (var point in _points)
        {
            DestroyImmediate(point);
        }

        _points.Clear();
    }

    public void ChangeRotation()
    {
        if (_sideClockWise == ClockWise.Clockwise)
        {
            _sideClockWise = ClockWise.Anti_Clockwise;
        }
        else
        {
            _sideClockWise = ClockWise.Clockwise;
        }

        SpawnPoints();
    }
}