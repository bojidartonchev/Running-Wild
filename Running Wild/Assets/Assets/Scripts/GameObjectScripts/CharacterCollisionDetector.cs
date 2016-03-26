using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterCollisionDetector : MonoBehaviour {

    private bool isDead;
	// Use this for initialization
	void Start () {
        isDead = false;
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
        if (collision.gameObject.tag == "Animal" &&  !isDead)
        {
            gameObject.AddComponent<DeathScript>();
            isDead = true;
        }
        if (collision.gameObject.tag == "Tree")
        {
            gameObject.AddComponent<TreeBump>();
        }
    }
}
