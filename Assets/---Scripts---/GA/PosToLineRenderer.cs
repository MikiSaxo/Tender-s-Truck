using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteInEditMode]
public class PosToLineRenderer : MonoBehaviour
{
    private LineRenderer _lineRend;
    [SerializeField] private Transform[] _transformArray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePos();
    }

    private void UpdatePos()
    {
        if (_lineRend == null)
        {
            _lineRend = GetComponent<LineRenderer>();
        }

        Vector3[] posArray = new Vector3[_transformArray.Length];
        for (int i = 0; i < _transformArray.Length; i++)
        {
            posArray[i] = _transformArray[i].position;
        }

        _lineRend.SetPositions(posArray);
    }
}
