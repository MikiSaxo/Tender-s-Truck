using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EditorSetGoodBPM : MonoBehaviour
{
    [SerializeField] private TMP_InputField _bpmFieldSet;
    [SerializeField] private TMP_InputField _bpmFieldSave;
    [SerializeField] private GameObject _startMappingBtn;



    public void OnClick()
    {
        if (_bpmFieldSet.text == "")
            return;
        
        var value = int.Parse(_bpmFieldSet.text);
        _bpmFieldSave.text = $"{value}";
        BoardManager.Instance.BPM = value;

        _startMappingBtn.SetActive(true);
    }
}
