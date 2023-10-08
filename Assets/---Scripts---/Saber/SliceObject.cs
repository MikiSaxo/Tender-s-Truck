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
    [SerializeField] private float _angleToDestroy;
    [SerializeField] private ElementType _currentType;
    [SerializeField] private GameObject _sauceStick;

    private Vector3 _previousPos;

    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(_startSlicePoint.position, _endSlicePoint.position, out RaycastHit hit,
            _sliceable);

        if (hasHit)
        {
            Vector3 firstPos = transform.position - _previousPos;
            Vector3 secondPos = hit.transform.up;

            var elementType = hit.transform.gameObject.GetComponent<ElementChild>().GetElementType();

            if ((elementType is ElementType.RedHorizontal or ElementType.RedVertical
                      && _currentType is ElementType.RedHorizontal or ElementType.RedVertical)
                     || (elementType is ElementType.YellowVertical or ElementType.YellowHorizontal
                         && _currentType is ElementType.YellowVertical or ElementType.YellowHorizontal))
            {
                if (Vector3.Angle(firstPos, secondPos) > _angleToDestroy ||
                    Vector3.Angle(firstPos, -secondPos) > _angleToDestroy)
                {
                    GameObject target = hit.transform.gameObject;
                    Slice(target);
                }
            }
        }

        _previousPos = transform.position;
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = _velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(_endSlicePoint.position - _startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(_endSlicePoint.position, planeNormal);

        if (hull != null)
        {
            // Be careful to not spawn frite in a object but in the world. Otherwise the hull won't have the good position
            GameObject upperHull = hull.CreateUpperHull(target, _sliceMat);
            // print($"upper pos : {upperHull.transform.position} / target pos : {target.transform.position}");
            SetupSliceComponent(upperHull, target.transform);

            GameObject lowerHull = hull.CreateLowerHull(target, _sliceMat);
            SetupSliceComponent(lowerHull, target.transform);

            Destroy(target);
            Destroy(upperHull, 5);
            Destroy(lowerHull, 5);
        }
    }

    public void SetupSliceComponent(GameObject sliceObj, Transform target)
    {
        Rigidbody rb = sliceObj.AddComponent<Rigidbody>();
        MeshCollider collider = sliceObj.AddComponent<MeshCollider>();
        collider.convex = true;
        // collider.isTrigger = true;

        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        sliceObj.transform.position = target.position;
        // rb.velocity = Vector3.zero;
        rb.AddExplosionForce(_cutForce, sliceObj.transform.position*1.1f, 1);
    }

    public void ChangeSauceType(ElementType newType)
    {
        _currentType = newType;
        _sauceStick.GetComponent<MeshRenderer>().material = PartyManager.Instance.GetElementTypeMat((int)newType);
    }
}