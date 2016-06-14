using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

namespace Assets.Assets.Scripts.Facebook
{
    public class FacebookLabelGenerator : MonoBehaviour
    {
        public GameObject player;
        public GameObject labelObject;

        private Dictionary<int,List<FacebookUser>> userContainer;
        
        // Use this for initialization
        void Start () {
	        this.userContainer = new Dictionary<int, List<FacebookUser>>();
            this.FillContainer();           
        }
	
        // Update is called once per frame
        void Update ()
        {
            int currentDistance = (int) player.transform.position.z;
            if (this.userContainer.ContainsKey(currentDistance))
            {
                var playersTillNow = this.userContainer[currentDistance];
               
                foreach (FacebookUser user in playersTillNow)
                {
                    this.SpawnLabel(user.ID, user.Name);
                }
                this.userContainer.Remove(currentDistance);
            }
        }

        private void SpawnLabel(string currId, string name)
        {
            if (currId.Equals(FacebookManager.Instance.UserId))
            {
                return;                
            }
            
            GameObject currentEntry = (GameObject) Instantiate(this.labelObject,new Vector3(this.player.transform.position.x, this.player.transform.position.y+10, this.player.transform.position.z+100),Quaternion.Euler(0f,0f,Random.Range(-15,15)));
            currentEntry.transform.SetParent(this.transform, false);

            Transform currentProfilePic = currentEntry.transform.Find("EntryProfilePic");
            SpriteRenderer avatarImage = currentProfilePic.GetComponent<SpriteRenderer>();
            currentEntry.transform.Find("EntryProfileName").GetComponent<TextMesh>().text = name;

            FB.API("/" + currId + "/picture?type=square&height=512&width=512", HttpMethod.GET,
                delegate (IGraphResult result)
                {
                    if (result.Error != null)
                    {
                        Debug.Log(result.Error);
                    }
                    else
                    {
                        avatarImage.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 512, 512), new Vector2());
                    }
                });
        }

        private void FillContainer()
        {
            if (FacebookManager.Instance.ScoreData != null)
            {
                var data = FacebookManager.Instance.ScoreData;

                foreach (Dictionary<string, object> element in data)
                {
                    var username = (element["user"] as Dictionary<string, object>)["name"].ToString();
                    var userId = (element["user"] as Dictionary<string, object>)["id"].ToString();
                    int score = int.Parse(element["score"].ToString());
                    
                    if (score <= PlayerPrefs.GetInt("highestScore"))
                    {
                        return;
                    }
                    score -= 100; //to spawn it 100 meters earlier
                    
                    if (!this.userContainer.ContainsKey(score))
                    {
                        this.userContainer.Add(score,new List<FacebookUser>());
                    }
                    this.userContainer[score].Add(new FacebookUser(username,userId,score));
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
            this.FillContainer();
        }
    }
}
