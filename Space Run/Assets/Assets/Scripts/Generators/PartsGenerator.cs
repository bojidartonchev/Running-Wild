using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Assets.Scripts.Generators
{
    public class PartsGenerator : MonoBehaviour
    {
        public List<GameObject> parts;
        public GameObject player;
        public float spawnDistance;
        public float spawnTime;
        public float minYOffset;
        public float maxYOffset;
        private int partsCount;


        // Use this for initialization
        void Start()
        {
            StartCoroutine(Spawner());
            this.partsCount = this.parts.Count;
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
                int randomPartIndex = UnityEngine.Random.Range(0, this.partsCount);
                GameObject currPart = Instantiate(this.parts[randomPartIndex]);
                currPart.transform.parent = this.transform;

                var randomOffset = Random.Range(this.minYOffset, this.maxYOffset);
                currPart.transform.position = new Vector3(player.transform.position.x, 1f, player.transform.position.z) + (transform.forward * this.spawnDistance) + (transform.right * randomOffset);
                
                yield return new WaitForSeconds(this.spawnTime);
            }
        }
    }
}

