using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMove : MonoBehaviour
{
    [SerializeField] private Transform _roadTransform;
    [SerializeField] private float _roadSpeed;
    [SerializeField] private CharacterBehaviour _characterBehaviour;
    private bool _isMoveRoad;

    private void Start()
    {
        _characterBehaviour.Fight.AddListener(StopMoveRoad);
    }

    private void Update()
    {
        MoveRoad();
    }

    private void MoveRoad()
    {
        if (_characterBehaviour._isGameState)
        {
            _isMoveRoad = true;
            _roadTransform.Translate(_roadTransform.forward * Time.deltaTime * _roadSpeed);
        }
    }

    private void StopMoveRoad()
    {
        _isMoveRoad = false;
        _roadSpeed = 0f;
    }

    private void OnDisable()
    {
        _characterBehaviour.Fight.RemoveListener(StopMoveRoad);
    }
}
