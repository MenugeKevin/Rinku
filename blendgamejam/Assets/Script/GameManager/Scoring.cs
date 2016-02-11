using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Scoring : MonoBehaviour {

    public static string _leaderboard = "";

    private static int _score = 0;
    private static float _multi = 1f;
    private static int _highscore = PlayerPrefs.GetInt("highscore", 0);
    // Use this for initialization
    void Start () {
        _multi = 1.0f;
        _score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    public static void onLogout()
    {
        if (Social.localUser.authenticated)
            ((PlayGamesPlatform)Social.Active).SignOut();
    }

    // Add to Score some number
    public static void AddToScore(int number)
    {
        _score += (number * (int)_multi);
    }

    // Get the Score
    public static long getScore()
    {
        return (_score);
    }

    public static long getHighScore()
    {
        return (_highscore);
    }

    public static void UpdateHighscore()
    {
        if (_score > _highscore)
        {
            PlayerPrefs.SetInt("highscore", _score);
            _highscore = _score;
            PlayerPrefs.Save();
        }
    }

    // set the multiplicator
    public static void incMultiplicator()
    {
        _multi++;
    }

    public static void resetMultiplicator()
    {
        _multi = 1.0f;
    }

    public static float getMulti()
    {
        return (_multi);
    }

   
}
