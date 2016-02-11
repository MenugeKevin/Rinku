using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreUI : MonoBehaviour {
    Text _highscoreUI;
    // Use this for initialization
    void Start()
    {
        _highscoreUI = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _highscoreUI.text = "HighScore : " + Scoring.getHighScore();
    }

}
