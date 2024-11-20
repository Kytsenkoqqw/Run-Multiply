using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform PlayerTransform;
    private int _numberOfStickmans;
    [SerializeField] private TextMeshPro _counterText;
    [SerializeField] private GameObject _stickman;
    

    private void Start()
    {
        PlayerTransform = transform;
        _numberOfStickmans = transform.childCount - 1;
        _counterText.text = _numberOfStickmans.ToString();
    }

    private void MakeStickman(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(_stickman, transform.position, Quaternion.identity, transform);
        }
        
        _numberOfStickmans = transform.childCount - 1;
        _counterText.text = _numberOfStickmans.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gate"))
        {
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;

            var gateManager = other.GetComponent<GateManager>();

            if (gateManager.Multiply)
            {
                MakeStickman(_numberOfStickmans * gateManager.RandomNumber);
            }
            else
            {
                MakeStickman(_numberOfStickmans * gateManager.RandomNumber);
            }
        }
    }
}
