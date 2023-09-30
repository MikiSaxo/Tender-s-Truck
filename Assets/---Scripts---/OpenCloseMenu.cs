using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseMenu : MonoBehaviour
{
    [Header("Timings")]
    [SerializeField] private float _openDuration;
    [SerializeField] private float _closeDuration;

    [Header("Setup")]
    [SerializeField] private GameObject _objToMove;
    [SerializeField] private Transform[] _tpPoints;
    [SerializeField] private bool _isClosed;


    private void OpenAnim()
    {
        _isClosed = false;
    }

    private void CloseAnim()
    {
        _isClosed = true;
    }

    public void MoveMenu()
    {
        if (_isClosed)
            OpenAnim();
        else
            CloseAnim();
    }

    private void Update()
    {
        if(!_isClosed)
            _objToMove.transform.position = Vector3.Lerp(_objToMove.transform.position, _tpPoints[1].position, Time.deltaTime * _openDuration);
        else
            _objToMove.transform.position = Vector3.Lerp(_objToMove.transform.position, _tpPoints[0].position, Time.deltaTime * _closeDuration);
    }
}
