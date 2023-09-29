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

            var friteType = hit.transform.gameObject.GetComponent<Frite>().GetFriteType();

            if (Vector3.Angle(firstPos, secondPos) > _angleToDestroy ||
                 Vector3.Angle(firstPos, -secondPos) > _angleToDestroy)
            {
                if ((friteType is ElementType.RedHorizontal or ElementType.RedVertical
                     && _currentType is ElementType.RedHorizontal or ElementType.RedVertical)
                    || (friteType is ElementType.YellowVertical or ElementType.YellowHorizontal
                        && _currentType is ElementType.YellowVertical or ElementType.YellowHorizontal))
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
            SetupSliceComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, _sliceMat);
            SetupSliceComponent(lowerHull);

            Destroy(target);
            Destroy(upperHull, 3);
            Destroy(lowerHull, 3);
        }
    }

    public void SetupSliceComponent(GameObject sliceObj)
    {
        Rigidbody rb = sliceObj.AddComponent<Rigidbody>();
        MeshCollider collider = sliceObj.AddComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = true;

        rb.AddExplosionForce(_cutForce, sliceObj.transform.position, 1);
    }

    public void ChangeSauceType(ElementType newType)
    {
        _currentType = newType;
        _sauceStick.GetComponent<MeshRenderer>().material = PartyManager.Instance.GetFriteType((int)newType);
    }
}