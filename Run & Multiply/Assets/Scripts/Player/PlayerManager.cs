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
    [Range(0, 1f)][SerializeField] private float  DistanceFactor, Radius;

    
    
    


    private void Start()
    {
        PlayerTransform = transform;
        _numberOfStickmans = transform.childCount - 1;
        _counterText.text = _numberOfStickmans.ToString();
    }

    private void FormatStickman()
    {
        for (int i = 0; i < PlayerTransform.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);
         
            var newPos = new Vector3(x, -0.10f, z);
            StartCoroutine(MoveToPosition(PlayerTransform.GetChild(i), newPos, 1f));
        }
    }

    private void MakeStickman(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(_stickman, transform.position, Quaternion.identity, transform);
        }
        
        _numberOfStickmans = transform.childCount - 1;
        _counterText.text = _numberOfStickmans.ToString();
        
        FormatStickman();
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
    
    private IEnumerator MoveToPosition(Transform child, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = child.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // Добавляем easing (аналог Ease.OutBack)
            t = EaseOutBack(t);

            child.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // Убедимся, что объект точно в целевой позиции
        child.localPosition = targetPosition;
    }
    
    private float EaseOutBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
    }
}
