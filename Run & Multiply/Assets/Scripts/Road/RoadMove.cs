using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMove : MonoBehaviour
{
    [SerializeField] private Transform _roadTransform;
    [SerializeField] private float _roadSpeed;
    [SerializeField] private CharacterBehaviour _characterBehaviour;
    [SerializeField] private bool _isMoveRoad;

    private void Awake()
    {
        _characterBehaviour.Fight.AddListener(StopMoveRoad);
        _isMoveRoad = true;
    }

    private void Update()
    {
        if (_isMoveRoad && _characterBehaviour._isGameState)
        {
            MoveRoad();
        }
    }

    private void MoveRoad()
    {
        _roadTransform.Translate(_roadTransform.forward * Time.deltaTime * _roadSpeed);
    }

    private void StopMoveRoad()
    {
        _isMoveRoad = false;
    }

    private void OnDisable()
    {
        _characterBehaviour.Fight.RemoveListener(StopMoveRoad);
    }
}
