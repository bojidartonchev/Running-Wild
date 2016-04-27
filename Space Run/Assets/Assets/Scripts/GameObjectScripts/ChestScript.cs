using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour
{

    public GameObject particle;

    public AudioClip chestOpen;

    private Camera cam;
    private bool startRotating;
    private int hashValue;
    private bool isOpened;
    // Use this for initialization
    void Start()
    {
        this.hashValue = Animator.StringToHash("OpenTrigger");
        this.isOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && !isOpened)
        {
            Invoke("OpenChest", 2);
            isOpened = true;
            GameObject particleSystem = Instantiate(particle, transform.position, Quaternion.identity) as GameObject;
            particleSystem.transform.SetParent(this.transform);

        }
    }

    void OpenChest()
    {
        AudioSource.PlayClipAtPoint(this.chestOpen, new Vector3(transform.position.x, transform.position.y, transform.position.z - 25));
        this.GetComponent<Animator>().SetTrigger(this.hashValue);
    }
}