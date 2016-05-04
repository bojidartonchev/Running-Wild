using Assets.Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Assets.Scripts.GameObjectScripts
{
    public class CharacterCollisionDetector : MonoBehaviour {
    
        public bool IsDead { get; private set; }
        // Use this for initialization
        void Start () {
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

            if (collision.gameObject.tag == "Chest")
            {
                this.CollectItem(collision.gameObject);
                Destroy(collision.gameObject);
            }
        }

        private void CollectItem(GameObject obj)
        {
            var partName = "LeftWingParts";
            //var partName = obj.transform.transform.GetChild(0).name;
            PlayerPrefs.SetInt(partName,PlayerPrefs.GetInt(partName)+1);
        }

    }
}
