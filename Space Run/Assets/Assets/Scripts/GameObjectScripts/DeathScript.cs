using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets.Assets.Scripts.GameObjectScripts
{
    public class DeathScript : MonoBehaviour {

        private Transform camera;
        private List<Quaternion> rotations;
        private Quaternion currentDeathRotation;
        // Use this for initialization
        void Start () {

            GameObject.Find("Player").GetComponent<FirstPersonController>().enabled = false;
            GameObject.Find("AnimalGenerator").SetActive(false);
            GameObject.Find("EnvironmentGenerator").SetActive(false);
            camera = GameObject.Find("MainCamera").GetComponent<Transform>();
            GameObject.Find("GameUI").transform.Find("GameOverUI").transform.gameObject.SetActive(true);
            rotations = new List<Quaternion>();
            rotations.Add(Quaternion.Euler(333, 252, 62));
            rotations.Add(Quaternion.Euler(312, 79, 328));
            rotations.Add(Quaternion.Euler(304, 331, 41));
            currentDeathRotation = rotations[Random.Range(0, 3)];
        }
	
        // Update is called once per frame
        void Update () {
            camera.rotation = Quaternion.Lerp(camera.rotation, currentDeathRotation, Time.deltaTime * 2);
        }
    }
}
