using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterCollisionDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
        SceneManager.LoadScene(0);
    }
}
