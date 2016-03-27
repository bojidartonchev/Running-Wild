using UnityEngine;
using System.Collections;

public class AnimalGenerator : MonoBehaviour
{
    public GameObject dinosaur;
    public GameObject player;
    public float spawnDistance;
    public float spawnTime;
    public float minYOffset;
    public float maxYOffset;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator Spawner()
    {
        bool flag = true;

        while (flag)
        {
            var randomOffset = Random.Range(this.minYOffset, this.maxYOffset);
            GameObject animal = Instantiate(dinosaur);
            animal.transform.position = new Vector3(player.transform.position.x, 5, player.transform.position.z) + (transform.forward * this.spawnDistance) + (transform.right * randomOffset);
            animal.transform.LookAt(player.transform);


            yield return new WaitForSeconds(this.spawnTime);
        }
    }
}

