using UnityEngine;
using UnityEngine.Events;
using System;

public class CoinTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent OnCoinCollected = new ();
    public bool isCoinCollected = false;
    void Start()
{
    var collider = GetComponent<Collider>();
    if (collider == null)
        Debug.LogError("No Collider found on coin!");
    else if (!collider.isTrigger)
        Debug.LogError("Collider is not set as a Trigger!");
        
    Debug.Log("CoinTrigger initialized on " + gameObject.name);
}
    void OnTriggerEnter(Collider other)
    {
        isCoinCollected = true;
        OnCoinCollected?.Invoke();
        Destroy(transform.parent.gameObject);
        Debug.Log("OnCoinCollected event invoked");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
