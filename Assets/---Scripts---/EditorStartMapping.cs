using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorStartMapping : MonoBehaviour
{
    [SerializeField] private GameObject _setup;
    
    public void OnClick()
    {
        _setup.SetActive(false);
        BoardManager.Instance.SpawnMap();
    }
}
