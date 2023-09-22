using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private Camera _mainCam;
    [SerializeField] private LayerMask _layerMask;

    // Update is called once per frame
    void Update()
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _layerMask))
        {
            transform.position = raycastHit.point;
        }
    }
}
