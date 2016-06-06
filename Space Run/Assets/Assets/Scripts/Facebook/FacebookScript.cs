using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Assets.Scripts.Facebook
{
    public class FacebookScript : MonoBehaviour
    {
        public GameObject dialogLoggedIn;
        public GameObject dialogLoggedOut;
        public GameObject profilePicture;
        public GameObject nameHolder;

        //leaderboard
        public GameObject leaderboardScroll;
        public GameObject leaderboardElement;

        public void FBlogin()
        {
            List<string> permissions = new List<string>();
            permissions.Add("public_profile");
            
            FB.LogInWithReadPermissions(permissions, AuthCallBack);
        }

        public void FBlogout()
        {
            FB.LogOut();
            this.DealWithFBMenus(FB.IsLoggedIn);
        }

        public void Share()
        {
            FacebookManager.Instance.Share();
        }

        public void Invite()
        {
            FacebookManager.Instance.Invite();  
        }

        public void ShareWithUsers()
        {
            FacebookManager.Instance.ShareWithUsers();
        }

        void AuthCallBack(IResult result)
        {
            if (result.Error != null)
            {
                Debug.Log(result.Error);
            }
            else
            {
                if (FB.IsLoggedIn)
                {
                    FacebookManager.Instance.IsLoggedIn = true;
                    FacebookManager.Instance.GetProfile();
                    Debug.Log("FB is logged in");
                }
                else
                {
                    Debug.Log("FB is NOT logged in");
                }
                this.DealWithFBMenus(FB.IsLoggedIn);
            }
        }

        void Awake()
        {
            FacebookManager.Instance.InitFB();
            this.OnInitialize();
        }

        private void OnInitialize()
        {
            if (FB.IsInitialized)
            {
                this.DealWithFBMenus(FB.IsLoggedIn);
            }
            else
            {
                StartCoroutine("WaitForInitialize");
            }
        }

        private void QueryScores()
        {
            FacebookManager.Instance.QueryScores();
            this.FillLeaderboard();
        }

        private void DealWithFBMenus(bool isLoggedIn)
        {
            if (isLoggedIn)
            {
                this.dialogLoggedIn.SetActive(true);
                this.dialogLoggedOut.SetActive(false);

                this.QueryScores();

                if (FacebookManager.Instance.ProfileName != null)
                {
                    Text userName = this.nameHolder.GetComponent<Text>();
                    userName.text = "Hi, " + FacebookManager.Instance.ProfileName;
                }
                else
                {
                    StartCoroutine("WaitForProfileName");
                }

                if (FacebookManager.Instance.ProfilePic != null)
                {
                    Image currentPic = this.profilePicture.GetComponent<Image>();
                    currentPic.sprite = FacebookManager.Instance.ProfilePic;
                }
                else
                {
                    StartCoroutine("WaitForProfilePic");
                }
            }
            else
            {
                this.dialogLoggedIn.SetActive(false);
                this.dialogLoggedOut.SetActive(true);

            }
        }
        
        private IEnumerator WaitForProfileName()
        {
            while (FacebookManager.Instance.ProfileName == null)
            {
                yield return null;
            }
            this.DealWithFBMenus(FacebookManager.Instance.IsLoggedIn);
        }

        private IEnumerator WaitForProfilePic()
        {
            while (FacebookManager.Instance.ProfilePic == null)
            {
                yield return null;
            }
            this.DealWithFBMenus(FacebookManager.Instance.IsLoggedIn);
        }

        private void FillLeaderboard()
        {
            if (FacebookManager.Instance.ScoreData != null)
            {
                var data = FacebookManager.Instance.ScoreData;

                foreach (Transform child in this.leaderboardScroll.transform)
                {
                    Destroy(child.gameObject);
                }

                foreach (Dictionary<string, object> element in data)
                {
                    var username = (element["user"] as Dictionary<string, object>)["name"];
                    var userId = (element["user"] as Dictionary<string, object>)["id"];
                    int score = int.Parse(element["score"].ToString());

                    GameObject currentEntry = Instantiate(this.leaderboardElement);
                    currentEntry.transform.SetParent(this.leaderboardScroll.transform,false);

                    Transform currentNameContainer = currentEntry.transform.Find("EntryProfileName");
                    Transform currentScoreContainer = currentEntry.transform.Find("EntryProfileScore");
                    Text nameText = currentNameContainer.GetComponent<Text>();
                    Text scoreText = currentScoreContainer.GetComponent<Text>();
                    nameText.text = username.ToString();
                    scoreText.text = score.ToString();

                    Transform currentProfilePic = currentEntry.transform.Find("EntryProfilePic");
                    Image avatarImage = currentProfilePic.GetComponent<Image>();

                    FB.API("/"+userId+"/picture?type=square&height=128&width=128", HttpMethod.GET,
                        delegate(IGraphResult result)
                        {
                            if (result.Error != null)
                            {
                                Debug.Log(result.Error);
                            }
                            else
                            {
                                avatarImage.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
                            }
                        });
                }
            }
            else
            {
                StartCoroutine("WaitForScrollData");
            }
        }

        private IEnumerator WaitForScrollData()
        {
            while (FacebookManager.Instance.ScoreData == null)
            {
                yield return null;
            }
            this.FillLeaderboard();
        }

        private IEnumerator WaitForInitialize()
        {
            while (!FB.IsInitialized)
            {
                yield return null;
            }
            this.OnInitialize();
        }

    }
}
