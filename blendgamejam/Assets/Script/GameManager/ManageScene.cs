using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class ManageScene : MonoBehaviour {

    public GameObject _submitText;
	// Use this for initialization
	void Start () {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void quitApplication()
    {
        Application.Quit();
    }

    public void restartApplication()
    {
        Application.LoadLevel(0);
    }

    public void submitHighscore()
    {
        Social.ReportScore(Scoring.getHighScore(), "CgkI0tnNw8EVEAIQAQ", (bool success) =>
        {
            if (success)
            {
                Debug.Log("Submit Successful");    
                _submitText.GetComponent<Text>().text = "OK!";
            }
            else
            {
                Debug.Log("Submit Failed");
                _submitText.GetComponent<Text>().text = "Error!";
            }
        });
    }

    public void logToGooglePlay()
    {
        if (Social.localUser.authenticated)
        {
            Application.LoadLevel(1);
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    DebugStatus.changeStatus("Login Failed");
                    Application.LoadLevel(1);
                }
                else
                {
                    DebugStatus.changeStatus("Login Failed");
                }
            });        
 
        }
    }
 
    void OnApplicationQuit()
    {
        Scoring.onLogout();
        PlayerPrefs.Save();
    }

    void OnApplicationPause()
    {
        Scoring.onLogout();
        PlayerPrefs.Save();
    }
}
