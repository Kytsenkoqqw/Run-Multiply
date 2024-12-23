using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;

public class PlayerManager : MonoBehaviour
{
    public Transform PlayerTransform;
    public int numberOfStickmans, numberOfEnemyStickman;
    [SerializeField] private TextMeshPro _counterText;
    [SerializeField] private GameObject _stickman;
    [SerializeField] private CharacterBehaviour _characterBehaviour;
    [Range(0, 1f)][SerializeField] private float  DistanceFactor, Radius;

    private void Start()
    {
        PlayerTransform = transform;
        numberOfStickmans = transform.childCount - 1;
        _counterText.text = numberOfStickmans.ToString();
      //  MakeStickman(90);
    }

    public void FormatStickman()
    {
        for (int i = 1; i < PlayerTransform.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);
         
            var newPos = new Vector3(x, -0.10f, z);
            StartCoroutine(MoveToPosition(PlayerTransform.GetChild(i), newPos, 0.5f));
        }
    }

    private void MakeStickman(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(_stickman, transform.position, Quaternion.identity, transform);
        }
        
        numberOfStickmans = transform.childCount - 1;
        _counterText.text = numberOfStickmans.ToString();
        
        FormatStickman();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gate"))
        {
            Debug.Log("Spawn");
            other.transform.parent.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
            other.transform.parent.GetChild(1).GetComponent<CapsuleCollider>().enabled = false;

            var gateManager = other.GetComponent<GateManager>();

            if (gateManager.Multiply)
            {
                MakeStickman(numberOfStickmans * gateManager.RandomNumber);
            }
            else
            {
                MakeStickman(numberOfStickmans * gateManager.RandomNumber);
            }
        }
    }
    
    private IEnumerator MoveToPosition(Transform child, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = child.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (child == null)
                yield break;
            
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            
            t = EaseOutBack(t);

            child.localPosition = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        if (child != null)
            child.localPosition = targetPosition;

    }
    
    private float EaseOutBack(float t)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return 1 + c3 * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2);
    }

    public void ChangeNumbers()
    {
        StartCoroutine(UpdatePlayerAndEnemyStickmansNumbers());
    }

    private IEnumerator UpdatePlayerAndEnemyStickmansNumbers()
    {
        if (_characterBehaviour._enemyTransform == null || _characterBehaviour._enemyTransform.GetChild(1) == null)
            yield break;
        
        
        numberOfEnemyStickman = _characterBehaviour._enemyTransform.transform.GetChild(1).childCount - 1;
        numberOfStickmans = transform.childCount - 1;

        while (numberOfEnemyStickman > 0)
        {
            numberOfEnemyStickman--;
            numberOfStickmans--;

            _characterBehaviour._enemyTransform.GetChild(1).GetComponent<EnemyManager>()._counterText.text =
                numberOfEnemyStickman.ToString();
            _counterText.text = numberOfStickmans.ToString();
            
            yield return null;
            
        }

        if (numberOfEnemyStickman == 0)
        {
            if (transform == null)
            {
                yield break;
            }
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.identity;
            }
        }
        
        _characterBehaviour._enemyTransform.gameObject.SetActive(false);
    }
}
