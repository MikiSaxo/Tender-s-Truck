using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SignalElementComing : MonoBehaviour
{
    [SerializeField] private GameObject _yellow;
    [SerializeField] private GameObject _red;

    private void Start()
    {
        UnableBoth();
    }

    private void ChangeSilhouette(bool isYellow)
    {
        _yellow.SetActive(isYellow);
        _red.SetActive(!isYellow);
    }

    private void UnableBoth()
    {
        _yellow.SetActive(false);
        _red.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ElementChild>())
        {
            var type = other.gameObject.GetComponent<ElementChild>().CurrentType;

            if (type == ElementType.YellowHorizontal || type == ElementType.YellowVertical)
                ChangeSilhouette(true);
            else if (type == ElementType.RedVertical || type == ElementType.RedHorizontal)
                ChangeSilhouette(false);
            // else
            //     UnableBoth();
        }
    }
}