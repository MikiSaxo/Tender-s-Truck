using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
        
    [Header("Setup")]
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _comboText;
    [Header("Combos Values")]
    [SerializeField] private float _frite;
    [SerializeField] private float _croquette;
    [SerializeField] private float _mozza;

    private float _currentScore;
    private float _currentCombo;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ElementToSpawn.LoseLife += LoseCombo;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
            AddPoints(ElementType.RedHorizontal);
    }

    public void AddPoints(ElementType type)
    {
        _currentCombo++;
        
        if (type == ElementType.Croquette)
        {
            _currentScore += _croquette * _currentCombo;
        }
        else if (type == ElementType.RedHorizontal || type == ElementType.RedVertical ||
                 type == ElementType.YellowHorizontal || type == ElementType.YellowVertical)
        {
            _currentScore += _frite * _currentCombo;
        }
        else if (type == ElementType.Mozza)
        {
            _currentScore += _mozza * _currentCombo;
        }
        
        UpdateTexts();
        PunchTexts();
    }

    public void LoseCombo()
    {
        _currentCombo = 1;
        
        UpdateTexts();
        RedTextCombo();
    }

    private void UpdateTexts()
    {
        _comboText.text = $"x{_currentCombo}";
        _scoreText.text = $"{_currentScore}";
    }

    private void PunchTexts()
    {
        transform.DOScale(1, 0);
        transform.DOPunchScale(Vector3.one, .25f);
    }

    private void RedTextCombo()
    {
        _comboText.DOColor(Color.red, 0);
        _comboText.DOColor(Color.white, 1);
    }

    private void OnDisable()
    {
        ElementToSpawn.LoseLife -= LoseCombo;
    }
}
