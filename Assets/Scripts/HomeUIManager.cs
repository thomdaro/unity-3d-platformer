using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour
{
    public void StartGame()
    {
        // Loads the first level to start the game
        SceneManager.LoadScene("Level1");
    }
}
