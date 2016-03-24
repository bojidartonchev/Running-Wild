using UnityEngine;
using System.Collections;

public class AnimalScript : MonoBehaviour {
    public Transform player;
    public float minDist;
    public float maxDist;
    public float moveSpeed;
	// Use this for initialization
	void Start () {
        this.player = GameObject.Find("FPSController").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(this.player);

        if (Vector3.Distance(transform.position, this.player.position) >= minDist)
        {

            transform.position += transform.forward * moveSpeed * Time.deltaTime;


            if (Vector3.Distance(transform.position, this.player.position) <= maxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
        if (transform.position.z < this.player.position.z)
        {
            Destroy(gameObject);
        }
    }
    
}
