using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ObjectZoom : MonoBehaviour
{
    [SerializeField] private float _zoomIn = .05f;
    [SerializeField] private float _zoomOut = 1.2f;
    [SerializeField] private GameObject[] objectsToMove;

    void Update()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");

        if (zoomInput == 0)
            return;

        foreach (var obj in objectsToMove)
        {
            var position = obj.transform.position;

            if (zoomInput < 0)
            {
                var newZ = Vector3.Lerp(position, transform.position, _zoomIn);
                position = new Vector3(position.x, position.y, newZ.z);
                obj.transform.position = position;
            }
            else
            {
                var newZ = Vector3.Lerp(position, transform.position, _zoomIn);
                position = new Vector3(position.x, position.y, newZ.z * _zoomOut);
                obj.transform.position = position;
            }
        }
    }
}