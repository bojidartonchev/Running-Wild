using UnityEngine;
using System.Collections;

public class SetKinematic : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Terrain")
            GetComponent<Rigidbody>().isKinematic = true;
    }
}
