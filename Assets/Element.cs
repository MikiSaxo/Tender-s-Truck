using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] private GameObject _frite;
    [SerializeField] private GameObject _point;

    public void Init(ElementType element, bool isEditor)
    {
        if (element == ElementType.Nothing)
            return;

        if (element == ElementType.Point)
        {
            _point.SetActive(true);
            _frite.SetActive(false);
            _point.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            _frite.SetActive(true);
            _point.SetActive(false);
            _frite.GetComponent<Frite>().Init(element, isEditor);
            _frite.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}