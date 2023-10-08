using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorRailCross : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    
    private bool _isMusicPlaying;

    private void Start()
    {
        BoardManager.Instance.LaunchTimeline += UpdateCollider;
    }

    private void UpdateCollider()
    {
        _isMusicPlaying = !_isMusicPlaying;
        _collider.enabled = _isMusicPlaying;
    }

    private void OnDisable()
    {
        BoardManager.Instance.LaunchTimeline -= UpdateCollider;
    }
}
