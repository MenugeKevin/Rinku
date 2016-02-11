using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUI : MonoBehaviour {

    Text _scoreUI;
	// Use this for initialization
	void Start () {
        _scoreUI = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        _scoreUI.text = "Score : " + Scoring.getScore();
	}
}
