using System.Collections;
using UnityEngine;

namespace Assets.Assets.Scripts.Generators
{
    public class ChestGenerator : MonoBehaviour
    {
        public GameObject chest;
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
                GameObject chestObj = Instantiate(chest);
                chestObj.transform.parent = transform;
                chestObj.transform.position = new Vector3(player.transform.position.x, 1f, player.transform.position.z) + (transform.forward * this.spawnDistance) + (transform.right * randomOffset);
                
                yield return new WaitForSeconds(this.spawnTime);
            }
        }
    }
}

