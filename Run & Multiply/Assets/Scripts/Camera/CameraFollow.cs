using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = new Vector3(0, 5,0);
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        transform.position = _target.position + _offset;
    }
}
