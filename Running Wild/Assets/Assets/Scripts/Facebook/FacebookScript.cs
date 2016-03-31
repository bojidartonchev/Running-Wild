using Facebook.Unity;
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

        public void FBlogin()
        {
            List<string> permissions = new List<string>();
            permissions.Add("public_profile");

            FB.LogInWithReadPermissions(permissions, AuthCallBack);
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
            FB.Init(SetInit,OnHideUnity);
        }

        void SetInit()
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("FB is logged in");
            }
            else
            {
                Debug.Log("FB is NOT logged in");
            }
            this.DealWithFBMenus(FB.IsLoggedIn);
        }

        void OnHideUnity(bool isGameShown)
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

        private void DealWithFBMenus(bool isLoggedIn)
        {
            if (isLoggedIn)
            {
                this.dialogLoggedIn.SetActive(true);
                this.dialogLoggedOut.SetActive(false);

                //FB.API("/me?fields=first_name",HttpMethod.GET, DisplayUsername);
                FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
            }
            else
            {
                this.dialogLoggedIn.SetActive(false);
                this.dialogLoggedOut.SetActive(true);
            }
        }

        private void DisplayProfilePic(IGraphResult result)
        {
            if (result.Texture != null)
            {
                Image profilePic = profilePicture.GetComponent<Image>();
                profilePic.sprite = Sprite.Create(result.Texture,new Rect(0,0,128,128), new Vector2() );
            }
        }
    }
}
