using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // static GameManager instance to read and modify game data
    public static GameManager instance = null;
    
    // current run's score
    public int score = 0;
    // overall high score
    public int highScore = 0;
    // the starting level (1 by default)
    public int currentLevel = 1;
    // the final level (loops back to 1 when completed)
    public int highestLevel = 2;

    private void Awake()
    {
        // assigns an instance to the GameManager object
        if (instance == null)
        {
            instance = this;
        }
        // if the instance changes, destroy it
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // when we change scenes, don't destroy the manager to preserve data
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseScore(int amount)
    {
        // increase the score
        score += amount;
        
        // if needed, update the high score
        if (score > highScore)
        {
            highScore = score;
        }
    }

    public void Reset()
    {
        // set score to 0, level to 1, and load the first level
        score = 0;
        currentLevel = 1;
        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void IncreaseLevel()
    {
        // if we're not at the end, go to the next level
        if (currentLevel < highestLevel)
        {
            currentLevel++;
        }
        else
        {
            // if we are, reset to level 1
            currentLevel = 1;
        }
        // load the selected level
        SceneManager.LoadScene("Level" + currentLevel);
    }
}
