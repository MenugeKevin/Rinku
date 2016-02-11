using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System.Collections.Generic;

public class ManageLeaderboard : MonoBehaviour {
    Text _text;
    string leaderboardList = "";
    ILeaderboard lb;
    List<string> valueScores = new List<string>();
    List<string> userIds = new List<string>();

	// Use this for initialization
	void Start () {
        _text = GetComponent<Text>();
        _text.text = "Loading Top 5 leaderboard";
        lb = Social.CreateLeaderboard();
        lb.id = "CgkI0tnNw8EVEAIQAQ";
        getTopFive();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void getTopFive()
    {
        lb.LoadScores(success =>
        {
            if (success)
            {
                if (lb.scores.Length < 1)
                {
                    _text.text = "Leaderboard is Empty\nSubmit your score now!";
                    return;
                }
                for (int i = 0; i < 5 && i < lb.scores.Length; ++i)
                {
                    valueScores.Add(lb.scores[i].value.ToString());
                    userIds.Add(lb.scores[i].userID);
                }                
                getNameFive();
            }
            else
            {
                Debug.Log("Failed to retrieve Leaderboard");
                _text.text = "Failed to retrieve Leaderboard";
            }
        });
    }

    void getNameFive()
    {
        if (Social.localUser.authenticated)
        {
            Social.LoadUsers(userIds.ToArray(), (users) =>
            {
                int i = 0;
                leaderboardList = "";
                foreach(IUserProfile profile in users)
                {
                    leaderboardList += (i + 1) + " - ";
                    leaderboardList += profile.userName + " - ";
                    leaderboardList += valueScores[i] + "\n";
                    i++;
                }
                _text.text = leaderboardList;
            });
        }
    }
}
