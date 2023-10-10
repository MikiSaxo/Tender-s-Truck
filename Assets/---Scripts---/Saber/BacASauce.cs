using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BacASauce : MonoBehaviour
{
    public static BacASauce Instance;
    // public event Action StickInSauceAction;

    [SerializeField] private GameObject[] _sliceObj;
    
    public ElementType CurrentType => _currentType;
    
    [SerializeField] private ElementType _currentType;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeSauce(ElementType whichFrite)
    {
        _currentType = whichFrite;
        gameObject.GetComponent<MeshRenderer>().material = PartyManager.Instance.GetElementTypeMat((int)_currentType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SauceStick>() != null)
        {
            _sliceObj[0].GetComponent<SliceObject>().ChangeSauceType(_currentType);
            _sliceObj[1].GetComponent<SliceObject>().ChangeSauceType(_currentType);
            // StickInSauceAction?.Invoke();
        }
    }
}
