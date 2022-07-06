using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadMainMenu()
    {
        FindObjectOfType<GameSession>().ResetGame();   
    }


}
