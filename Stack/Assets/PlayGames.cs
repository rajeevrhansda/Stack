using System;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;



public class PlayGames : MonoBehaviour
{
    string leaderboardID = "CgkI4Y7QxskIEAIQAQ";
    string achievementID = "CgkI4Y7QxskIEAIQAA";
    public static PlayGamesPlatform platform;

    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {

            }
            else
            {

            }
        });
        UnlockAchievement();
    }

    public void AddScoreToLeaderboard(int highscore)
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportScore(highscore, leaderboardID, success => { });
        }
    }
    public void ShowLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowAchievementsUI();
        }
    }

    public void UnlockAchievement()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportProgress(achievementID, 100f, success => { });
        }
    }
}