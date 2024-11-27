using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    
    # region Singleton

    // Singleton
    public static GameManager instance;

    // Awake function
    private void Awake()
    {
        // Check if instance is null
        if (instance == null) instance = this;
    }

    # endregion

    // Initialise
    public float currentScore = 0f;
    public SavedData savedData;
    public bool isPlaying = false;
    

    // Unity event
    public UnityEvent on_Play = new UnityEvent();
    public UnityEvent on_GameOver = new UnityEvent();

    // Start game
    private void Start()
    {
        string saveString = SavedSystem.Load("save"); // Load save
        if (saveString != null) // Check if save string is not null
        { 
            savedData = JsonUtility.FromJson<SavedData>(saveString); // Load saved data
        }
        else
        {
            savedData = new SavedData();
        }
    } 
    private void Update()
    {
        // Check if game is playing
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
        }

    }

    // Start game
    public void StartGame()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null in Spawner.Start!");
            return;
        }
        // Invoke on_Play
        on_Play.Invoke();
        // Set current score to 0
        currentScore = 0f; 
        isPlaying = true;
    }

    // Reset high score
    public void ResetHighScore()
    {
        savedData.highScore = 0;
        string saveString = JsonUtility.ToJson(savedData);
        SavedSystem.Save("save", saveString);
    } 

    // Game over
    public void GameOver()
    {
        if (currentScore > savedData.highScore) // Check if current score is greater than saved data high score
        {
            savedData.highScore = currentScore; // Set saved data high score to current score
            string saveString = JsonUtility.ToJson(savedData); // Convert saved data to json
            SavedSystem.Save("save", saveString); // Save data
        }
        isPlaying = false; // Set is playing to false
        FindObjectOfType<Spawner>()?.ClearObstacles(); 
        on_GameOver.Invoke(); // Invoke on game over 
    }

    // Pretty score
    public string PrettyScore()
    {
        // Return "Score:  score"
        return "Score: " + Mathf.RoundToInt(currentScore).ToString();
    }

    public string PrettyHighScore()
    {
        // Return "Score:  score"
        return "Score: " + Mathf.RoundToInt(savedData.highScore).ToString();
    }

}