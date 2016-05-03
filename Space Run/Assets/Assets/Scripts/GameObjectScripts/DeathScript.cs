using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets.Assets.Scripts.GameObjectScripts
{
    public class DeathScript : MonoBehaviour
    {

        private Transform camera;
        private List<Quaternion> rotations;
        private Quaternion currentDeathRotation;
        private FrostEffect frost;
        // Use this for initialization
        void Start()
        {
            GameObject.Find("Player").GetComponent<FirstPersonController>().enabled = false;
            GameObject.Find("AnimalGenerator").SetActive(false);
            GameObject.Find("EnvironmentGenerator").SetActive(false);
            camera = GameObject.Find("MainCamera").GetComponent<Transform>();
            frost = camera.GetComponent<FrostEffect>();
            frost.enabled = true;
            frost.FrostAmount = 0.2f;
            StartCoroutine("FreezeScreen");
            GameObject.Find("GameUI").transform.Find("GameOverUI").transform.gameObject.SetActive(true);
            rotations = new List<Quaternion>();
            rotations.Add(Quaternion.Euler(333, 252, 62));
            rotations.Add(Quaternion.Euler(312, 79, 328));
            rotations.Add(Quaternion.Euler(304, 331, 41));
            currentDeathRotation = rotations[Random.Range(0, 3)];

        }

        IEnumerator FreezeScreen()
        {
            while (true)
            {
                frost.FrostAmount += 0.004f;
                if (frost.FrostAmount >= 0.35f)
                {
                    break;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        // Update is called once per frame
        void Update()
        {
            camera.rotation = Quaternion.Lerp(camera.rotation, currentDeathRotation, Time.deltaTime * 2);
        }
    }
}