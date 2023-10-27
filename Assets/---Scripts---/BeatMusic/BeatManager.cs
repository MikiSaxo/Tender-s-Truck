using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    public static BeatManager Instance;
    
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Intervals[] _intervals;
    
    public bool CanGo { get; set; }
    public int BPM  { get; set; }
            
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!CanGo)
            return;
        
        foreach (Intervals interval in _intervals) 
        {
            float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetIntervalLength(BPM)));
            interval.CheckForNewInterval(sampledTime);
        }
    }
}



[Serializable]
public class Intervals
{
    [SerializeField] private float _steps;
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;

    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * _steps);
    }

    public void CheckForNewInterval(float interval)
    {

        if(Mathf.FloorToInt(interval) != _lastInterval) 
        {
            _lastInterval = Mathf.FloorToInt(interval);
            _trigger.Invoke();
        }
    }
    //public UnityEvent GetTrigger()
    //{
    //    return _trigger;
    //}
}
