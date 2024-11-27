using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CharacterBehaviour : MonoBehaviour
{
    public UnityEvent Fight;
    public bool _isMoveByTouch, _isGameState;
    private Vector3 _mouseStartPosition, _playerStartPosition;
    public float MoveSpeed;
    private Camera _camera;
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private PlayerManager _playerManager;
    
    
    public bool _isAttack;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_isAttack)
        {
            Fight?.Invoke();
            var enemyDirection = new Vector3(_enemyTransform.position.x, transform.position.y, _enemyTransform.position.z) - transform.position;
            
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation, quaternion.LookRotation(enemyDirection, Vector3.up),Time.deltaTime * 3f);
            }
            
        }
        else
        {
            MoveToPlayer();
        }
        
    }

    private void MoveToPlayer()
    {
        if (Input.GetMouseButtonDown(0) && _isGameState)
        {
            _isMoveByTouch = true;

            var plane = new Plane(Vector3.up, 0f);

            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                _mouseStartPosition = ray.GetPoint(distance + 1f);
                _playerStartPosition = transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMoveByTouch = false;
        }

        if (_isMoveByTouch)
        {
            var plane = new Plane(Vector3.up, 0f);
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                var mousePos = ray.GetPoint(distance + 1f);
                var move = mousePos - _mouseStartPosition;
                var control = _playerStartPosition + move;

                if (_playerManager._numberOfStickmans > 50)
                {
                    control.x = Mathf.Clamp(control.x, -5f, 5f);
                }
                else
                {
                    control.x = Mathf.Clamp(control.x, -7f, 7f);
                }

                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * MoveSpeed ), transform.position.y, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemyZone"))
        {
            _enemyTransform = other.transform;
            _isAttack = true;
        }
    }
}
