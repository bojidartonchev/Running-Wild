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
            this.DealWithFBMenus(FB.IsLoggedIn);
        }       

        private void DealWithFBMenus(bool isLoggedIn)
        {
            if (isLoggedIn)
            {
                this.dialogLoggedIn.SetActive(true);
                this.dialogLoggedOut.SetActive(false);

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

    }
}
