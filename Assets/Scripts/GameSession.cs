using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour 
{
    
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerScore = 0;

    [SerializeField] TextMeshProUGUI livesText; 
    [SerializeField] TextMeshProUGUI scoreText; 
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() 
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            Invoke("TakeLife", 2f);  //added delay to see death animaton with Invoke!
        }

        else
        {
            ResetGameSession();
        }

    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    public void ResetGameSession()
    {
        
        StartCoroutine(DelayRestartScene());
        
    }

    public void AddToScore(int pointsToAdd)
    {
        playerScore += pointsToAdd;
        scoreText.text = playerScore.ToString();
    }

    IEnumerator DelayRestartScene()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void ResetGame()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
