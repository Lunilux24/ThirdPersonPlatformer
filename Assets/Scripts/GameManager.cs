using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    private CoinTrigger[] coins;
    void Start()
    {
        coins = FindObjectsByType<CoinTrigger>((FindObjectsSortMode)FindObjectsInactive.Include);

        // Attach IncrementScore to each coin's OnCoinCollision event
        foreach (CoinTrigger coin in coins)
        {
            coin.OnCoinCollision.AddListener(IncrementScore);
        }
    }

    void IncrementScore()
    {
        score++;
        scoreText.text = $"Coins Collected: {score}";
        //Debug.Log($"Score: {score}");
    }
}
