using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class GateManager : MonoBehaviour
{
    public TextMeshPro GateNo;
    public int RandomNumber;
    public bool Multiply;

    private void Start()
    {
        if (Multiply)
        {
            RandomNumber = Random.Range(0, 3);
            GateNo.text = "X" + RandomNumber;
        }
        else
        {
            RandomNumber = Random.Range(10, 100);

            if (RandomNumber % 2 != 0)
            {
                RandomNumber += 1;
            }
            
            GateNo.text = "+" + RandomNumber.ToString();
        }
    }
}
