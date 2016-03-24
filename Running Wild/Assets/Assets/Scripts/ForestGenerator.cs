using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForestGenerator : MonoBehaviour
{
    public GameObject forest1;
    public GameObject forest2;
    public GameObject player;
    public float spawnDistance;
    public float minYOffset;
    public float maxYOffset;
    public float minSpawnTime;
    public float maxSpawnTime;

    private List<GameObject> forests;

    // Use this for initialization
    void Start()
    {
        forests = new List<GameObject>();
        forests.Add(forest1);
        forests.Add(forest2);
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
            int randomForestIndex = UnityEngine.Random.Range(0, 2);
            GameObject currForest = Instantiate(this.forests[randomForestIndex]);
            var randomOffset = Random.Range(this.minYOffset, this.maxYOffset);
            var randomTime = Random.Range(this.minSpawnTime, this.maxSpawnTime);
            currForest.transform.position = new Vector3(player.transform.position.x, player.transform.position.y-1.2f, player.transform.position.z) + (transform.forward * this.spawnDistance) + (transform.right * randomOffset);
            currForest.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            yield return new WaitForSeconds(randomTime);
        }
    }


    private int[] GetRandomParams()
    {
        int[] toReturn = new int[3];
        toReturn[0] = UnityEngine.Random.Range((int)this.player.transform.position.x - 400, (int)this.player.transform.position.x + 400); //X limit
        toReturn[1] = UnityEngine.Random.Range((int)this.player.transform.position.z + 200, (int)this.player.transform.position.z + 1000); //Z Limit      
        return toReturn;
    }

}

