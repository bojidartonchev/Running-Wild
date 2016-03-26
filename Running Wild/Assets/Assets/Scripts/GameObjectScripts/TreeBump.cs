using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class TreeBump : MonoBehaviour {
    public Transform camera;
    public GameObject arms;
    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Transform>();
        //GameObject.Find("Arms").GetComponent<Animator>().Play("Idle");
        camera.rotation = Quaternion.Slerp(camera.rotation, Quaternion.Euler(280  , 201, 160), 0.1f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
