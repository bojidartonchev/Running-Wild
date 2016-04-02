using UnityEngine;
using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System;
using Facebook.MiniJSON;

public class FacebookManager : MonoBehaviour {

    private static FacebookManager instance;
    private Dictionary<string,object> scoresList; 

    public static FacebookManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject fbm = new GameObject("FBManager");
                fbm.AddComponent<FacebookManager>();
            }
            return instance;
        }
    }

    public bool IsLoggedIn { get; set; }
    public string ProfileName { get; set; }
    public Sprite ProfilePic { get; set; }
    public string AppLinkUrl { get; set; }
    public List<object> ScoreData { get; set; } 
	
    public void InitFB()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(SetInit, OnHideUnity);
        }
        else
        {
            this.IsLoggedIn = FB.IsLoggedIn;
        }
    }

    public void GetProfile()
    {
        FB.API("/me?fields=first_name",HttpMethod.GET, DisplayUsername);
        FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        FB.GetAppLink(DealWithAppLink);
    }

    public void Share()
    {
        FB.FeedShare(
            string.Empty,
            new Uri(this.AppLinkUrl),
            "Hello this is the title",
            "Hello this is the caption",
            "Check out this game",
            new Uri("http://image.way2enjoy.com/pic/45/77/99/7/600full-little-caprice.jpg"),
            string.Empty,
            ShareCallback
            );
    }

    public void Invite()
    {
        FB.Mobile.AppInvite(
            new Uri(this.AppLinkUrl),
            new Uri("http://image.way2enjoy.com/pic/45/77/99/7/600full-little-caprice.jpg"),
            InviteCallback
            );
    }

    public void ShareWithUsers()
    {
        FB.AppRequest(
            "Come and join me, I bet you can't beat my score!",
            null,
            new List<object>() {"app_users"},
            null,
            null,
            null,
            null,
            ShareWithUsersCallback
            );
    }

    public void QueryScores()
    {
        FB.API("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, ScoresCallback);
    }

    public void SetScore()
    {
        var scoreData = new Dictionary<string,string>();
        scoreData["score"] = UnityEngine.Random.Range(10, 200).ToString();
        FB.API("/me/scores",HttpMethod.POST, SetScoreCallback,scoreData);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        //IsLoggedIn = true;
    }

    private void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is logged in");
            this.GetProfile();
        }
        else
        {
            Debug.Log("FB is NOT logged in");
        }
        this.IsLoggedIn = FB.IsLoggedIn;
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        {
            this.ProfilePic = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
    }

    private void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            this.ProfileName = "" + result.ResultDictionary["first_name"];
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    private void DealWithAppLink(IAppLinkResult result)
    {
        if (!String.IsNullOrEmpty(result.Url))
        {
            this.AppLinkUrl = "" + result.Url;
        }
        else
        {
            this.AppLinkUrl = "http://google.com"; //In case of an error
        }
    }

    private void ShareCallback(IResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("Share canceled");
        }
        else if (!String.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Error on share");
        }
        else if (!String.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log("Success on share");
        }
    }

    private void InviteCallback(IResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("Invite canceled");
        }
        else if (!String.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Error on invite");
        }
        else if (!String.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log("Success on invite");
        }
    }

    private void ShareWithUsersCallback(IAppRequestResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("Challange canceled");
        }
        else if (!String.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Error on challange");
        }
        else if (!String.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log("Success on challange");
        }
    }

    private void ScoresCallback(IResult result)
    {
        this.ScoreData = (Json.Deserialize(result.RawResult) as Dictionary<string, object> )["data"] as List<object>;
    }

    private void SetScoreCallback(IResult result)
    {
        Debug.Log("" + result.RawResult);
    }
}
