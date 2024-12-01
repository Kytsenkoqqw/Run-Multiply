using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;


public class EnemyManager : MonoBehaviour
{
    public TextMeshPro _counterText;
    [SerializeField] private GameObject _stickman;
    
    [Range(0f, 1f)] [SerializeField]
    private float _distanceFactor, _radius;

    public Transform enemyTransform;
    public bool IsEnemyAttack;
    
    private void Start()
    {
        for (int i = 0; i < UnityEngine.Random.Range(20,120); i++)
        {
            Instantiate(_stickman, transform.position, new Quaternion(0f, 180f, 0f, 1f), transform);
        }

        _counterText.text =  (transform.childCount - 1).ToString();
        
        FormatStickman();
    }

    private void Update()
    {
        if (IsEnemyAttack && transform.childCount > 1)
        {
            var enemyPos = new Vector3(enemyTransform.position.x, transform.position.y, enemyTransform.position.z);
            var enemyDirection = enemyTransform.position - transform.position;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(1).rotation = Quaternion.Slerp(transform.GetChild(i).rotation,
                    Quaternion.LookRotation(enemyDirection, Vector3.up), Time.deltaTime * 3f);

                var distance = enemyTransform.GetChild(1).position - transform.GetChild(i).position;

                if (distance.magnitude < 9f)
                {
                    transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position, enemyTransform.GetChild(1).position, Time.deltaTime * 2f);
                }
            }
        }
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

    public void Attack(Transform enemyForce)
    {
        Debug.Log("attack");
        enemyTransform = enemyForce;
        IsEnemyAttack = true;
    }
}
