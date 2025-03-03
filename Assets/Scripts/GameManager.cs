using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private CoinTrigger[] coins;
    void Start()
    {
        coins = FindObjectsOfType<CoinTrigger>(true);

        foreach (CoinTrigger coin in coins)
        {
            coin.OnCoinCollected.AddListener(IncrementScore);
        }
    }

    private void IncrementScore()
    {
        score++;
        Debug.Log("Score: " + score);
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
