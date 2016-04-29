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
            SceneManager.LoadScene(1);
        }

        public void GoToGarage()
        {
            SceneManager.LoadScene(3);
        }
    }
}
