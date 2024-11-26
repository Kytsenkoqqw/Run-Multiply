using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro _counterText;
    [SerializeField] private GameObject _stickman;
    
    [Range(0f, 1f)] [SerializeField]
    private float _distanceFactor, _radius;

    private void Start()
    {
        for (int i = 0; i < UnityEngine.Random.Range(20,120); i++)
        {
            Instantiate(_stickman, transform.position, new Quaternion(0f, 180f, 0f, 1f), transform);
        }

        _counterText.text =  (transform.childCount - 1).ToString();
        
        FormatStickman();
    }
    
    private void FormatStickman()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = _distanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * _radius);
            var z = _distanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * _radius);
         
            var newPos = new Vector3(x, -0.10f, z);
            transform.GetChild(i).localPosition = newPos;
        }
    }

    /*
    private void Update()
    {
        FormatStickman();
    }*/
}
