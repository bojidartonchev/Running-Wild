using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour {

    public Transform player;

    // Use this for initialization
    void Start () {
        this.player = GameObject.Find("FPSController").GetComponent<Transform>();

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z +200< this.player.position.z)
        {
            //Destroy(gameObject); this destroys the game object and GO needs to be reloaded in order to build another forest.
        }
    }
}
