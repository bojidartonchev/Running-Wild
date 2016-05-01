using UnityEngine;
using System.Collections;

public class AnimalScript : MonoBehaviour
{
    public Transform player;
    public float minDist;
    public float maxDist;
    public float moveSpeed;
    public bool shouldDestroy;
    // Use this for initialization
    void Start()
    {
        if (player == null)
        {
            this.player = GameObject.Find("Player").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(this.player);

        if (Vector3.Distance(transform.position, this.player.position) >= minDist)
        {

            transform.position += transform.forward * moveSpeed * Time.deltaTime;


            if (Vector3.Distance(transform.position, this.player.position) <= maxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }

        if (transform.position.z < this.player.position.z && shouldDestroy)
        {
            Destroy(gameObject);
        }
    }

}
