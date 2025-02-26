using System;
using UnityEngine;
using UnityEngine.Events;

public class CoinTrigger : MonoBehaviour
{
    public UnityEvent OnCoinCollision = new();
    public bool isCoinCollected = false;
    private void OnTriggerEnter(Collider triggeredObject)
    {
        isCoinCollected = true;
        OnCoinCollision?.Invoke();
        Debug.Log($"{gameObject.name} has been collected!");
    }
}
