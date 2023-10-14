using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EzySlice;
using Unity.VisualScripting;

public class SliceObject : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private VelocityEstimator _velocityEstimator;
    [SerializeField] private GameObject _sauceStick;
    [SerializeField] private float _velocityToDestroy;
    [Space(20f)]
    [Header("-- Old -- ")]
    [Header("Points")]
    [SerializeField] private Transform _startSlicePoint;
    [SerializeField] private Transform _endSlicePoint;
    [Header("Other")]
    [SerializeField] private float _cutForce;
    [SerializeField] private float _angleToDestroy;
    [SerializeField] private Material _sliceMat;
    [SerializeField] private LayerMask _sliceable;
    [Header("Temp")]
    [SerializeField] private ElementType _currentType;

    private Vector3 _previousPos;

    private void Start()
    {
        // BacASauce.Instance.StickInSauceAction += ChangeSauceType;
        //print("pos : " + Vector3.Distance(_startSlicePoint.position, _endSlicePoint.position));
    }

    void FixedUpdate()
    {
        //CutAngle();
    }

    // void CutAngle()
    // {
    //     bool hasHit = Physics.Linecast(_startSlicePoint.position, _endSlicePoint.position, out RaycastHit hit,
    //         _sliceable);
    //
    //     if (hasHit)
    //     {
    //         Vector3 firstPos = transform.position - _previousPos;
    //         Vector3 secondPos = hit.transform.up;
    //
    //         var elementType = hit.transform.gameObject.GetComponent<ElementChild>().CurrentType;
    //
    //         // print($"mine : {_currentType} / other : {elementType}");
    //         if ((elementType is ElementType.RedHorizontal or ElementType.RedVertical
    //              && _currentType is ElementType.RedHorizontal or ElementType.RedVertical)
    //             || (elementType is ElementType.YellowVertical or ElementType.YellowHorizontal
    //                 && _currentType is ElementType.YellowVertical or ElementType.YellowHorizontal))
    //         {
    //             print("angle : " + Vector3.Angle(firstPos, secondPos));
    //             if (Vector3.Angle(firstPos, secondPos) > _angleToDestroy ||
    //                 Vector3.Angle(firstPos, -secondPos) > _angleToDestroy)
    //             {
    //                 GameObject target = hit.transform.gameObject;
    //                 Slice(target);
    //             }
    //         }
    //     }
    //
    //     _previousPos = transform.position;
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Frite>())
        {
            var friteType = other.gameObject.GetComponent<ElementChild>().CurrentType;
            if ((_currentType == ElementType.RedHorizontal &&
                 (friteType == _currentType || friteType == ElementType.RedVertical))
                || (_currentType == ElementType.YellowHorizontal &&
                    (friteType == _currentType || friteType == ElementType.YellowVertical))
               )
            {
                // print("brr brr frite : " + _currentType + " / " + friteType);
                float velo = Vector3.Magnitude(_velocityEstimator.GetVelocityEstimate());
                print($"velo = {velo}");
                
                if(velo >= _velocityToDestroy)
                    Slice(other.gameObject);
            }
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = _velocityEstimator.GetVelocityEstimate();
        print("ça cut ");
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

            LifeManager.Instance.WinLife();
            ScoreManager.Instance.AddPoints(ElementType.RedHorizontal);
        }
    }

    public void SetupSliceComponent(GameObject sliceObj, Transform target)
    {
        Rigidbody rb = sliceObj.AddComponent<Rigidbody>();
        MeshCollider collider = sliceObj.AddComponent<MeshCollider>();
        collider.convex = true;
        // collider.isTrigger = true;

        //rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        // rb.velocity = Vector3.zero;
        sliceObj.transform.position = target.position;
        // rb.AddExplosionForce(_cutForce, sliceObj.transform.position, 1);
    }

    public void ChangeSauceType(ElementType newType)
    {
        _currentType = newType;
        _sauceStick.GetComponent<MeshRenderer>().material = PartyManager.Instance.GetElementTypeMat((int)_currentType);
    }

    private void OnDisable()
    {
        // BacASauce.Instance.StickInSauceAction -= ChangeSauceType;
    }
}