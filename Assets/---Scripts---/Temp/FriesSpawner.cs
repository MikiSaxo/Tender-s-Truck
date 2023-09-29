using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FriesSpawner : MonoBehaviour
{
    [SerializeField] private float _bpm;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _fritePrefab;
    [SerializeField] private float _currentTime;
    [SerializeField] private FriteIntervals[] _intervals;

    private int _intervalIndex;
    private int _dirIndex;

    private void Update()
    {
        return;

        //if (_intervalIndex >= _intervals.Length)
        //    return;

        //var interval = _intervals[_intervalIndex];

        //_currentTime += Time.deltaTime;

        //if(_currentTime >= interval.GetIntervalLength(_bpm))
        //{
        //    _currentTime -= _currentTime;

        //    SpawnFrite(_intervals[_intervalIndex].GetDirections()[_dirIndex]);
        //    _dirIndex++;


        //    if(_dirIndex >= _intervals[_intervalIndex].GetDirections().Length || _intervals[_intervalIndex].GetDirections().Length == 0)
        //    {
        //        _intervalIndex++;
        //        _dirIndex = 0;
        //    }


        //    //print("oui : " + _intervalIndex + " interval " + interval.GetIntervalLength(_bpm));
        //}

    }

    // public void SpawnFrite(Direction side)
    // {
    //     GameObject go = Instantiate(_fritePrefab);
    //     print("spawn frite : " + side);
    // }
}


[Serializable]
public class FriteIntervals
{
    [SerializeField] private float _steps;
    // [SerializeField] private Direction[] _directions;
    //[SerializeField] private UnityEvent _trigger;
    private int _lastInterval;

    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * _steps);
    }

    public void CheckForNewInterval(float interval)
    {

        if (Mathf.FloorToInt(interval) != _lastInterval)
        {
            _lastInterval = Mathf.FloorToInt(interval);
            //_trigger.Invoke();
        }
    }
    //public UnityEvent GetTrigger()
    //{
    //    return _trigger;
    //}
    // public Direction[] GetDirections()
    // {
    //     return _directions;
    // }
}
