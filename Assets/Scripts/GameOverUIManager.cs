using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{

    // displays the score for this round and high score as formatted text
    public Text score;
    public Text highScore;

    void Start()
    {
        // gets the score and high score from our game manager
        score.text = GameManager.instance.score.ToString();
        highScore.text = GameManager.instance.highScore.ToString();
    }

    public void RestartGame()
    {
        // Reset() zeroes out the score and starts us at level 1
        GameManager.instance.Reset();
    }
}
