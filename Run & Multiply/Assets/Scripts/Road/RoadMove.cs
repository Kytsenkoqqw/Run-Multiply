using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMove : MonoBehaviour
{
    private float _roadSpeed = 6f;
    [SerializeField] private Transform _roadTransform;
    [SerializeField] private CharacterBehaviour _characterBehaviour;
    

    private void Update()
    {
        MoveRoad();
        Debug.Log(_roadSpeed);
    }

    private void MoveRoad()
    {
        _roadTransform.Translate(_roadTransform.forward * Time.deltaTime * _roadSpeed);
        if (_characterBehaviour._isAttack)
        {
            ChangeRoadSpeed(2f);
        }
        else
        {
            ChangeRoadSpeed(6f);
        }
    }

    public void ChangeRoadSpeed(float roadSpeed)
    {
        _roadSpeed = roadSpeed;
    }
    
}
