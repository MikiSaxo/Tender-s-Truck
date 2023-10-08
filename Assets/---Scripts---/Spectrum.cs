using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    [Range(0.0000001f, 0.1f)]
    [HideInInspector] public float refValue = 0.0045f;

    [Range(0.1f, 5f)]
    [HideInInspector] public float scaleMultiplier = .6f;

    [Range(0.01f, 1f)]
    [HideInInspector] public float releaseTime = 0.7f;
    
    [Range(0.0000001f, 0.1f)]
    [SerializeField] private float _refValue = 0.0045f;

    [Range(0.1f, 5f)]
    [SerializeField] private float _scaleMultiplier = .6f;

    [Range(0.01f, 1f)]
    [SerializeField] private float _releaseTime = 0.7f;


    public AudioSource audioSource;
    public SpectrumElement spectrumElementPrefab;

    private SpectrumElement[] spectrumElements = new SpectrumElement[92];
    private float[] spectrum = new float[2048];


    private void Awake()
    {
        CreateElements();
        AlignElementsHorizontal();
        
        
    }

    private void Update()
    {
        refValue = _refValue;
        scaleMultiplier = _scaleMultiplier;
        releaseTime = _releaseTime;
        
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 0; i < spectrumElements.Length; i++)
        {
            var value = 20f * Mathf.Log10(spectrum[i + 2] / refValue);
            spectrumElements[i].SetScale(value);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            AlignElementsCircular();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            AlignElementsHorizontal();
        }
    }

    private void CreateElements()
    {
        for (int i = 0; i < spectrumElements.Length; i++)
        {
            spectrumElements[i] = Instantiate(spectrumElementPrefab, transform, false);
        }

        //AlignElementsHorizontal();
        AlignElementsCircular();
    }

    private void AlignElementsHorizontal()
    {
        var shiftPerElement = 1.5f;
        var leftShift = shiftPerElement * spectrumElements.Length / 2f;

        for (int i = 0; i < spectrumElements.Length; i++)
        {
            var element = spectrumElements[i];
            element.transform.localRotation = Quaternion.identity;
            element.transform.localPosition = Vector3.right * (i * shiftPerElement - leftShift);
        }
    }

    private void AlignElementsCircular()
    {
        var radius = 20f;
        var angleStep = 360f / spectrumElements.Length;

        int angleMultiplier = 0;

        for (int i = 0; i < spectrumElements.Length; i += 2)
        {
            spectrumElements[i].transform.localRotation = Quaternion.identity;
            spectrumElements[i + 1].transform.localRotation = Quaternion.identity;

            //Left
            spectrumElements[i].transform.Rotate(Vector3.forward, angleMultiplier * -angleStep);
            spectrumElements[i].transform.localPosition = spectrumElements[i].transform.up * radius;

            //Right
            spectrumElements[i + 1].transform.Rotate(Vector3.forward, (angleMultiplier + 1) * angleStep);
            spectrumElements[i + 1].transform.localPosition = spectrumElements[i + 1].transform.up * radius;

            angleMultiplier++;
        }
    }
}
