using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class playerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private AudioClip gameOverAudio;
    private int score = 0;
    private int wave = 0;
    private bool gameOver = false;
    public string mode = "basic";

    // UnityEvent for the mode change
    public UnityEvent OnModeChanged;

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
        
    }

    public void Wave(int amount)
    {
        wave = amount;
        waveText.text = "Wave: " + wave;
        this.gameObject.GetComponent<AudioSource>().PlayOneShot(this.gameObject.GetComponent<AudioSource>().clip);
        AddScore(5);

    }
    public void GameOver()
    {
        gameOver = true;
        gameOverText.text = "Game Over\n\nScore: " + score + "\nWave: " + wave + "\n\nPress Space to exit";
        gameOverPanel.SetActive(true);
        mainCamera.GetComponent<AudioSource>().clip = gameOverAudio;
    }
    private void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    public void ChangeMode(string newMode)
    {
        mode = newMode;
        OnModeChanged.Invoke();
    }
}
