using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EzySlice;
using TMPro;
using Unity.VisualScripting;

public class SliceObject : MonoBehaviour
{
    [Header("Setup")] [SerializeField] private VelocityEstimator _velocityEstimator;
    [SerializeField] private GameObject _sauceStick;
    [SerializeField] private GameObject _slashPrefab;

    [Space(20f)] [Header("-- Old -- ")] 
    [Header("Points")]
    [SerializeField] private Transform _startSlicePoint;
    [SerializeField] private Transform _endSlicePoint;
    [Header("Other")] [SerializeField] private float _cutForce;
    // [SerializeField] private float _angleToDestroy;
    [SerializeField] private Material[] _sliceMat;
    [SerializeField] private LayerMask _sliceable;
    [Header("Temp")] [SerializeField] private ElementType _currentType;
    // [SerializeField] private TMP_Text _debug;

    private Vector3 _previousPos;
    private float _velocityToCut;
    private Material _currentSliceMat;

    private void Start()
    {
        _velocityToCut = PartyManager.Instance.VelocityMinToCut;
        // StartCoroutine(OuiAngle());

        // BacASauce.Instance.StickInSauceAction += ChangeSauceType;
        //print("pos : " + Vector3.Distance(_startSlicePoint.position, _endSlicePoint.position));
    }

    void FixedUpdate()
    {
        // CutAngle();
        // print($"rota {transform.rotation.eulerAngles}");
        //Vector3 velocity = _velocityEstimator.GetVelocityEstimate();
        //_debug.text = $"{Math.Abs(Mathf.Round(velocity.x))}  /  {Math.Abs(Mathf.Round(velocity.y))}    /   {Math.Abs(Mathf.Round(velocity.z))}";
    }

    private Vector3 RoundVector(Vector3 vector3, int decimalPlaces)
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }

        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }

    void CutAngle()
    {
        bool hasHit = Physics.Linecast(_startSlicePoint.position, _endSlicePoint.position, out RaycastHit hit,
            _sliceable);

        if (hasHit)
        {
            Vector3 firstPos = transform.position - _previousPos;
            Vector3 secondPos = hit.transform.up;

            var elementType = hit.transform.gameObject.GetComponent<ElementChild>().CurrentType;

            // print($"mine : {_currentType} / other : {elementType}");
            if ((elementType is ElementType.RedHorizontal or ElementType.RedVertical
                 && _currentType is ElementType.RedHorizontal or ElementType.RedVertical)
                || (elementType is ElementType.YellowVertical or ElementType.YellowHorizontal
                    && _currentType is ElementType.YellowVertical or ElementType.YellowHorizontal))
            {
                // print("angle : " + Vector3.Angle(firstPos, secondPos));
                // if (Vector3.Angle(firstPos, secondPos) > _angleToDestroy ||
                //     Vector3.Angle(firstPos, -secondPos) > _angleToDestroy)
                // {
                // }
                // print("angle attack : " + transform.rotation.eulerAngles.z);
                // if (elementType is ElementType.RedHorizontal or ElementType.YellowHorizontal
                //     && transform.rotation.eulerAngles.z is >= 50 and <= 130 ||
                //     transform.rotation.eulerAngles.z is >= 230 and <= 310
                //     || elementType is ElementType.RedVertical or ElementType.YellowVertical
                //     && transform.rotation.eulerAngles.z is >= 0 and <= 40 && transform.rotation.eulerAngles.z >= 320
                //     || transform.rotation.eulerAngles.z is >= 140 and <= 220
                //    )
                print("nelson bonsoir");
                var velo = _velocityEstimator.GetVelocityEstimate();
                if (elementType is ElementType.RedHorizontal or ElementType.YellowHorizontal
                    && Math.Abs(velo.x) > _velocityToCut
                    || elementType is ElementType.RedVertical or ElementType.YellowVertical
                    && Math.Abs(velo.y) > _velocityToCut)
                {
                    GameObject target = hit.transform.gameObject;
                    Slice(target);
                }
            }
        }

        _previousPos = transform.position;
    }

    public void TrySlice(ElementType friteType, GameObject friteObj)
    {
        if ((_currentType == ElementType.RedHorizontal &&
             (friteType == ElementType.RedHorizontal || friteType == ElementType.RedVertical))
            || (_currentType == ElementType.YellowHorizontal &&
                (friteType == ElementType.YellowHorizontal || friteType == ElementType.YellowVertical))
           )
        {
            var velo = _velocityEstimator.GetVelocityEstimate();
            // print("bonne frite : " + velo);
            if (friteType is ElementType.RedHorizontal or ElementType.YellowHorizontal
                && (Math.Abs(velo.x*2) + Math.Abs(velo.z)) > _velocityToCut
                || friteType is ElementType.RedVertical or ElementType.YellowVertical
                && (Math.Abs(velo.y*2) + Math.Abs(velo.z)) > _velocityToCut)
            {
                if (friteType is ElementType.RedHorizontal or ElementType.RedVertical)
                    _currentSliceMat = _sliceMat[1];
                else if (friteType is ElementType.YellowHorizontal or ElementType.YellowVertical)
                    _currentSliceMat = _sliceMat[0];
                
                Slice(friteObj);
            }
        }
    }

    public void Slice(GameObject target)
    {
        var newTarget = target.GetComponent<Frite>().FriteObj;
        Vector3 velocity = _velocityEstimator.GetVelocityEstimate();
        print("ça cut ");
        Vector3 planeNormal = Vector3.Cross(_endSlicePoint.position - _startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = newTarget.Slice(_endSlicePoint.position, planeNormal);
        
        AudioManager.Instance.PlaySword();

        if (hull != null)
        {
            // Be careful to not spawn frite in a object but in the world. Otherwise the hull won't have the good position
            GameObject upperHull = hull.CreateUpperHull(newTarget, _currentSliceMat);
            // print($"upper pos : {upperHull.transform.position} / target pos : {target.transform.position}");
            SetupSliceComponent(upperHull, newTarget.transform);

            GameObject lowerHull = hull.CreateLowerHull(newTarget, _currentSliceMat);
            SetupSliceComponent(lowerHull, newTarget.transform);

            GameObject go = Instantiate(_slashPrefab);
            go.transform.position = target.transform.position;
            
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
        rb.AddExplosionForce(_cutForce, sliceObj.transform.localPosition, 2);
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