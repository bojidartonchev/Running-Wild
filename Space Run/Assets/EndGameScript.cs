using UnityEngine;
using System.Collections;

public class EndGameScript : MonoBehaviour {

    public GameObject innerEngine1;
    public GameObject innerEngine2;
    public GameObject outerEngine1;
    public GameObject outerEngine2;
    public GameObject mainEngine;
    public GameObject leftPipe;
    public GameObject rightPipe;
    public GameObject leftCyllinder;
    public GameObject rightCyllinder;
    public GameObject ship;

    private bool Up = false;
    private float shake = 0.05f;
    private float shipSpeed = 0.01f;
    private Coroutine shipCoroutine;
    private bool isFlying = false;

    // Use this for initialization
    void Start () {
        shipCoroutine = StartCoroutine(ShakeObject(ship));
        StartCoroutine(ActivateObject(innerEngine1, 3.5f));
        StartCoroutine(ActivateObject(innerEngine2, 3.5f));
        StartCoroutine(ActivateRigidBody(leftPipe,6f,10));
        StartCoroutine(ActivateObject(outerEngine1, 8));
        StartCoroutine(ActivateObject(outerEngine2, 8));
        StartCoroutine(ActivateRigidBody(rightPipe, 11,10));
        StartCoroutine(ActivateObject(mainEngine, 15.6f));
        StartCoroutine(ActivateRigidBody(rightCyllinder, 20.5f));
        StartCoroutine(ActivateRigidBody(leftCyllinder, 20.6f));
        StartCoroutine(ShipTakeOff(21.3f));
    }

    IEnumerator ActivateObject(GameObject obj, float delayTime)
    {
         yield return new WaitForSeconds(delayTime);
         obj.SetActive(true);
    }

    IEnumerator ActivateRigidBody(GameObject obj,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.transform.parent = null;
    }

    IEnumerator ActivateRigidBody(GameObject obj, float delayTime,float force)
    {
        yield return new WaitForSeconds(delayTime);
        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.transform.parent = null;
        obj.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(force/2, force * Random.Range(0, 6), force/3+1), new Vector3(obj.transform.position.x+5, obj.transform.position.y, obj.transform.position.z + 10));
    }

    IEnumerator ShakeObject(GameObject obj)
    {
        float shakeTimer = 0.1f;
        while (true)
        {
            yield return new WaitForSeconds(shakeTimer);
            shakeTimer -= 0.0001f;
            if (Up)
            {
                obj.transform.Translate(shake, shake, 0);
                this.Up = false;
            }
            else
            {
                obj.transform.Translate(-shake, -shake, 0);
                this.Up = true;
            }
        }
    }
    IEnumerator ShipTakeOff(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        isFlying = true;
        StopCoroutine(shipCoroutine);

    }

    void Update()
    {
        if (isFlying)
        {
            ship.transform.position = new Vector3(this.ship.transform.position.x, this.ship.transform.position.y + shipSpeed, this.ship.transform.position.z);
            this.shipSpeed += 0.001f;
            this.ship.GetComponent<AudioSource>().volume -= 0.001f;
        }
    }
}
