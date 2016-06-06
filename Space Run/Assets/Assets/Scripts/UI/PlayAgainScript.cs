using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Assets.Scripts.UI
{
    public class PlayAgainScript : MonoBehaviour {

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void PlayAgain()
        {
            GameObject.Find("Player").GetComponent<DistanceCounter>().SaveHighestScore();
            SceneManager.LoadScene(3);
        }

        public void GoToGarage()
        {
            GameObject.Find("Player").GetComponent<DistanceCounter>().SaveHighestScore();
            SceneManager.LoadScene(4);
        }
    }
}
