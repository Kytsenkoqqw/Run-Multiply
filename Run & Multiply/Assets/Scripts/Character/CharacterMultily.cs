using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterMultily : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMultiply;
    [SerializeField] private GameObject _manPrefab;
    
    [SerializeField] private char[] _symbols = {'X', '÷', '+', '-'};
    
    private int  _randomCount;
    private int _randomIndex;

    private void Start()
    {
        InitializeText();
    }

    private void InitializeText()
    {
        _countMultiply = GetComponent<TextMeshProUGUI>();

        _randomCount = Random.Range(1, 21);

        _randomIndex = Random.Range(0, _symbols.Length);

        _countMultiply.text = _symbols[_randomIndex] + _randomCount.ToString();
    }


}


