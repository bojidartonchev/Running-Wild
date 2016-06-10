using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Generators
{
    public class EnvironmentGenerator : MonoBehaviour
    {
        public GameObject object1;
        public GameObject object2;
        public GameObject player;
        public float spawnDistance;
        public float minYOffset;
        public float maxYOffset;
        public float minSpawnTime;
        public float maxSpawnTime;

        private List<GameObject> environments;

        // Use this for initialization
        void Start()
        {
            this.environments = new List<GameObject>();
            this.environments.Add(this.object1);
            this.environments.Add(this.object2);
            this.StartCoroutine(this.Spawner());
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
                int randomEnvironmentIndex = UnityEngine.Random.Range(0, 2);
                GameObject currEnvironment = Instantiate(this.environments[randomEnvironmentIndex]);
                currEnvironment.transform.parent = this.transform;
                var randomOffset = Random.Range(this.minYOffset, this.maxYOffset);
                var randomTime = Random.Range(this.minSpawnTime, this.maxSpawnTime);

                currEnvironment.transform.position = new Vector3(this.player.transform.position.x, -3f, this.player.transform.position.z) + (this.transform.forward * this.spawnDistance) + (this.transform.right * randomOffset);
                currEnvironment.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
          
                yield return new WaitForSeconds(randomTime);
            }

        }

    }
}

