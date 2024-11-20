using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Plane = UnityEngine.Plane;
using Vector3 = UnityEngine.Vector3;

public class CharacterBehaviour : MonoBehaviour
{
    public bool _isMoveByTouch, _isGameState;
    private Vector3 _mouseStartPosition, _playerStartPosition;
    public float MoveSpeed;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        MoveToPlayer();
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
                
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, control.x, Time.deltaTime * MoveSpeed ), transform.position.y, transform.position.z);
            }
        }
    }


}
