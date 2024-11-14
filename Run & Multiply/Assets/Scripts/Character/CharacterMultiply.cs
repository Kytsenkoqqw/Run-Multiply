using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterMultiply : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMultiply;
    [SerializeField] private GameObject _manPrefab;
    [SerializeField] private Transform _spawnPoint;
    
    
    [SerializeField] private char[] _symbols = {'X', '÷', '+', '-'};
    
    private int  _randomCount;
    private int _randomIndex;
    private List<GameObject> _spawnedMen = new List<GameObject>();

    private void Start()
    {
        
        InitializeText();
        
    }

    private void InitializeText()
    {
        _countMultiply = FindObjectOfType<TextMeshProUGUI>();

        _randomCount = Random.Range(1, 21);

        _randomIndex = Random.Range(0, _symbols.Length);

        _countMultiply.text = _symbols[_randomIndex] + _randomCount.ToString();
    }

    public void ApplyOperation()
    {
        char operation = _symbols[_randomIndex];
        int count = _randomCount;
        
        switch (operation)
        {
            case '+':
                SpawnMen(count); 
                break;

            case '-':
                RemoveMen(count); 
                break;

            case 'X':
                MultiplyMen(count);
                break;

            case '÷':
                DivideMen(count);
                break;

            default:
                Debug.LogError("Неизвестная операция: " + operation);
                break;
        }
    }
    
    private void SpawnMen(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newMan = Instantiate(_manPrefab, _spawnPoint.position, Quaternion.identity);
            _spawnedMen.Add(newMan);
        }
        Debug.Log("Спавнено мужиков: " + count);
    }
    
    private void RemoveMen(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_spawnedMen.Count > 0)
            {
                GameObject manToRemove = _spawnedMen[_spawnedMen.Count - 1];
                _spawnedMen.Remove(manToRemove);
                Destroy(manToRemove);
            }
        }
        Debug.Log("Удалено мужиков: " + count);
    }
     
    private void MultiplyMen(int multiplier)
    {
        int currentCount = _spawnedMen.Count;
        for (int i = 0; i < currentCount * (multiplier - 1); i++)
        {
            GameObject newMan = Instantiate(_manPrefab, _spawnPoint.position, Quaternion.identity);
            _spawnedMen.Add(newMan);
        }
        Debug.Log("Умножено мужиков на: " + multiplier);
    }

    private void DivideMen(int divisor)
    {
        int removeCount = _spawnedMen.Count - (_spawnedMen.Count % divisor);
        RemoveMen(removeCount);
        Debug.Log("Поделено мужиков на: " + divisor);
    }


}


