using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EzySlice;
using Unity.VisualScripting;

public class SliceObject : MonoBehaviour
{
    [SerializeField] private Transform _startSlicePoint;
    [SerializeField] private Transform _endSlicePoint;
    [SerializeField] private LayerMask _sliceable;
    [SerializeField] private VelocityEstimator _velocityEstimator;
    [SerializeField] private Material _sliceMat;
    [SerializeField] private float _cutForce;


    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(_startSlicePoint.position, _endSlicePoint.position, out RaycastHit hit, _sliceable);

        if(hasHit )
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = _velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(_endSlicePoint.position - _startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(_endSlicePoint.position, planeNormal);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, _sliceMat);
            SetupSliceComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, _sliceMat);
            SetupSliceComponent(lowerHull);

            Destroy(target);
        }
    }

    public void SetupSliceComponent(GameObject sliceObj)
    {
        Rigidbody rb = sliceObj.AddComponent<Rigidbody>();
        MeshCollider collider = sliceObj.AddComponent<MeshCollider>();
        collider.convex = true;

;        rb.AddExplosionForce(_cutForce, sliceObj.transform.position, 1);
    }
}
