using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BacASauce : MonoBehaviour
{
    [SerializeField] private ElementType _currentType;
    [SerializeField] private SliceObject _saber;
    public void ChangeSauce(ElementType whichFrite)
    {
        _currentType = whichFrite;
        gameObject.GetComponent<MeshRenderer>().material = PartyManager.Instance.GetElementTypeMat((int)_currentType);
    }

    public ElementType GetCurrentType()
    {
        return _currentType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SauceStick>() != null)
        {
            _saber.ChangeSauceType(_currentType);
        }
    }
}
