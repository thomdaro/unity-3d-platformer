using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    // label showing the current score
    public Text scoreLabel;

    void Start()
    {
        // refreshes once on initialization
        Refresh();
    }

    public void Refresh()
    {
        // will always initialize to the default score (usually 0)
        scoreLabel.text = "Score: " + GameManager.instance.score;
    }
}
