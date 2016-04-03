using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

    public GameObject particle;

    private Camera cam;
    private bool startRotating;
    private int hashValue;
    private bool isOpened;
	// Use this for initialization
	void Start () {
        this.hashValue = Animator.StringToHash("OpenTrigger");
        this.isOpened = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && !isOpened)
        {
            Invoke("ExecuteAnimation", 2);
            isOpened = true;
            Instantiate(particle, transform.position, Quaternion.identity);
            
        }
    }

    void ExecuteAnimation()
    {
        this.GetComponent<Animator>().SetTrigger(this.hashValue);
    }
}
