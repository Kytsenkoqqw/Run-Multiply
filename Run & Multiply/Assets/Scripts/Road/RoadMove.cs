using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMove : MonoBehaviour
{
    [SerializeField] private Transform _roadTransform;
    [SerializeField] private float _roadSpeed;
    [SerializeField] private CharacterBehaviour _characterBehaviour;

    private void Update()
    {
        MoveRoad();
    }

    private void MoveRoad()
    {
        if (_characterBehaviour._isGameState)
        {
            _roadTransform.Translate(_roadTransform.forward * Time.deltaTime * _roadSpeed);
        }
    }
}
