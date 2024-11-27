using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    // Initialise
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject instructionUI;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreUI;
    GameManager gameManager;

    AudioManager audioSource;

    private void Awake()
    {
        audioSource = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioManager>();
    }

    // Update is called once per frame  
    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.on_GameOver.AddListener(ActivateGameOver);
    }

    // Play button handler
    public void PlayButtonHandler()
    {
        audioSource.PlaySFX(audioSource.clickSFX);
        gameManager.StartGame();
    }

    // Reset HighScore button handler
    public void ResetHighScoreButtonHandler()
    {
        audioSource.PlaySFX(audioSource.clickSFX);
        gameManager.ResetHighScore();
    }

    // Quit button handler
    public void QuitButtonHandler()
    {
        audioSource.PlaySFX(audioSource.clickSFX);
        Application.Quit();
    }

    // Activate game over
    public void ActivateGameOver()
    {
        gameOverUI.SetActive(true);
        gameOverScoreUI.text = "Current Score: " + gameManager.PrettyScore();
        gameOverHighScoreUI.text = "High Score: " + gameManager.PrettyHighScore();
    }

    // Start button handler
    public void StartButtonHandler()
    {
        audioSource.PlaySFX(audioSource.clickSFX);
        startMenuUI.SetActive(false);
        instructionUI.SetActive(true);
    }

    // Back button handler
    public void BackButtonHandler()
    {
        audioSource.PlaySFX(audioSource.clickSFX);
        gameOverUI.SetActive(false);
        startMenuUI.SetActive(true);
    }

    private void OnGUI()
    {
        scoreUI.text = gameManager.PrettyScore();
    }
}
