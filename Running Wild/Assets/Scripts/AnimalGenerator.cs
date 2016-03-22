using UnityEngine;
using System.Collections;

public class AnimalGenerator : MonoBehaviour {
    public GameObject horse;
    public GameObject player;
    public float spawnDistance;
    public float spawnTime;
    public float minYOffset;
    public float maxYOffset;

    
    // Use this for initialization
    void Start () {
        StartCoroutine(Spawner());        
    }
	
	// Update is called once per frame
	void Update () {
       
    }
    public IEnumerator Spawner()
    {
        bool flag = true;

        while (flag)
        {
            var randomOffset = Random.Range(this.minYOffset, this.maxYOffset);
            GameObject newHorse = Instantiate(horse);
            newHorse.transform.position = player.transform.position + (transform.forward * this.spawnDistance) + (transform.right*randomOffset);
            newHorse.transform.LookAt(player.transform);
                     

            yield return new WaitForSeconds(this.spawnTime);
        }
    }
}

