using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets.Assets.Scripts.GameObjectScripts
{
    public class DeathScript : MonoBehaviour {

        private Transform camera;

        // Use this for initialization
        void Start () {
            GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
            GameObject.Find("AnimalGenerator").SetActive(false);
            GameObject.Find("EnvironmentGenerator").SetActive(false);
            camera = GameObject.Find("Main Camera").GetComponent<Transform>();
            GameObject.Find("GameUI").transform.Find("GameOverUI").transform.gameObject.SetActive(true);
        }
	
        // Update is called once per frame
        void Update () {
            camera.rotation = Quaternion.Lerp(camera.rotation, Quaternion.Euler(333, 252, 62), Time.deltaTime * 2);
        }
    }
}
