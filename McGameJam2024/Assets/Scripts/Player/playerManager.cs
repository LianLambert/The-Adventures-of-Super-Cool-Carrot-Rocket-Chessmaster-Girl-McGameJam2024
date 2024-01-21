using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    [SerializeField] public GameObject bishopHat;
    [SerializeField] public GameObject rookHat;
    [SerializeField] public GameObject queenHat;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    private int score = 0;

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;

    }

    public void Wave(int amount)
    {
        waveText.text = "Wave: " + amount;

    }
    public void GameOver()
    {

    }
}
