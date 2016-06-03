﻿using Assets.Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Assets.Scripts.GameObjectScripts
{

    public class CharacterCollisionDetector : MonoBehaviour {

        public AudioClip collectClip;

        public int CurrentRunParts { get; private set; }
        public bool IsDead { get; private set; }
        // Use this for initialization
        void Start () {
            this.CurrentRunParts = 0;
            this.IsDead = false;
        }
	
        // Update is called once per frame
        void Update () {
        
        }

        void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Animal")
            {
                collision.gameObject.GetComponent<Animation>().Play("hpunch");
                collision.gameObject.GetComponent<AudioSource>().Play();
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.tag == "Animal" &&  !this.IsDead)
            {
                gameObject.AddComponent<DeathScript>();
                gameObject.GetComponent<DistanceCounter>().SaveHighestScore();
                this.IsDead = true;
            }
            if (collision.gameObject.tag == "Tree")
            {
                gameObject.AddComponent<TreeBump>();
            }

            if (collision.gameObject.tag == "Part")
            {
                if (this.collectClip != null)
                {
                    AudioSource.PlayClipAtPoint(this.collectClip, this.transform.localPosition);
                }
                
                this.CollectItem(collision.gameObject);
                Destroy(collision.gameObject);
            }
        }

        private void CollectItem(GameObject obj)
        {
            this.CurrentRunParts++;
            var partName = obj.transform.name.Replace("(Clone)","");
            Debug.Log(partName);
            PlayerPrefs.SetInt(partName,PlayerPrefs.GetInt(partName)+1);
        }

    }
}
