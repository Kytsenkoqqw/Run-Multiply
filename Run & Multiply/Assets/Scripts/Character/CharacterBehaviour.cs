using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] private float _force = 2f;
    [SerializeField] private float _maxSpeed = 5f;
    
    [SerializeField] private float _sideSpeed = 2f;
    
    private Rigidbody _rigidbodyCharacter;
    
    
    private void Start()
    {
        _rigidbodyCharacter = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveCharacter();
        SideMove();
    }

    private void MoveCharacter()
    {
        _rigidbodyCharacter.AddForce(Vector3.forward * _force);
        
        if (_rigidbodyCharacter.velocity.magnitude > _maxSpeed)
        {
            _rigidbodyCharacter.velocity = _rigidbodyCharacter.velocity.normalized * _maxSpeed;
        }
    }

    private void SideMove()
    {
        
        float sideSpeed = Input.GetAxis("Horizontal") * _sideSpeed * Time.deltaTime;
        transform.Translate(sideSpeed,0,0);

    }
}
