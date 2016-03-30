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
                collision.gameObject.GetComponent<Animation>().Play("Attack");
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
        }    
    }
}
