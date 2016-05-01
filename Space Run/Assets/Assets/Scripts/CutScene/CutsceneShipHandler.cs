using UnityEngine;
using System.Collections;

public class CutsceneShipHandler : MonoBehaviour
{

    public GameObject explosion;
    public GameObject secondExplosion;
    public GameObject engineFire;

    public GameObject backWing;
    public GameObject sideWing;
    public GameObject firstEngine;
    public GameObject secondEngine;

    public AudioClip smallBomb;
    public AudioClip bigBomb;

    //Scene transition
    public string scene;
    public Color myColor;

    private Transform ship;

    void Start()
    {
        this.ship = GetComponent<Transform>();
        InvokeRepeating("MoveTheShip", 0, 0.05f);
        Invoke("ExecuteBackExplosion",9f);
    }

    void MoveTheShip()
    {
        ship.localPosition = new Vector3(ship.localPosition.x - 0.09f, ship.localPosition.y - 0.06f, ship.localPosition.z - 0.10f);

    }

    void ExecuteBackExplosion()
    {
        AudioSource.PlayClipAtPoint(smallBomb, new Vector3(222.4f, 138f, -210f),0.8f);
        secondExplosion.SetActive(true);
        this.backWing.transform.parent = null;
        this.sideWing.transform.parent = null;
        this.firstEngine.transform.parent = null;
        this.secondEngine.transform.parent = null;
        InvokeRepeating("BreakShip", 0, 0.05f);
        Invoke("ExecuteFrontExplosion",2.8f);
        Invoke("ExecuteFire", 4f);
    }

    void ExecuteFrontExplosion()
    {
        AudioSource.PlayClipAtPoint(bigBomb, new Vector3(222.4f, 138f, -210f),0.8f);
        explosion.SetActive(true);
        Invoke("ChangeLevel", 10f);
    }

    void ExecuteFire()
    {
        this.engineFire.SetActive(true);
    }

    void BreakShip()
    {
        this.backWing.transform.position = new Vector3(this.backWing.transform.position.x-0.04f, this.backWing.transform.position.y-0.01f, this.backWing.transform.position.z + 0.01f);
        this.backWing.transform.Rotate(new Vector3(this.backWing.transform.rotation.x + 1f, this.backWing.transform.rotation.y + 0.1f, this.backWing.transform.rotation.z + 1.5f));

        this.sideWing.transform.position = new Vector3(this.sideWing.transform.position.x + 0.02f, this.sideWing.transform.position.y + 0.01f, this.sideWing.transform.position.z + 0.01f);
        this.sideWing.transform.Rotate(new Vector3(this.sideWing.transform.rotation.x + 0.5f, this.sideWing.transform.rotation.y + 0.2f, this.sideWing.transform.rotation.z + 1.0f));

        this.firstEngine.transform.position = new Vector3(this.firstEngine.transform.position.x -0.01f, this.firstEngine.transform.position.y + 0.01f, this.firstEngine.transform.position.z - 0.01f);
        this.firstEngine.transform.Rotate(new Vector3(this.firstEngine.transform.rotation.x - 1f, this.firstEngine.transform.rotation.y + 0.1f, this.firstEngine.transform.rotation.z + 1.5f));

        this.secondEngine.transform.position = new Vector3(this.secondEngine.transform.position.x + 0.02f, this.secondEngine.transform.position.y - 0.03f, this.secondEngine.transform.position.z - 0.01f);
        this.secondEngine.transform.Rotate(new Vector3(this.secondEngine.transform.rotation.x - 0.5f, this.secondEngine.transform.rotation.y + 0.2f, this.secondEngine.transform.rotation.z + 1.0f));
    }

    void ChangeLevel()
    {
        Initiate.Fade(scene, myColor, 0.3f);
    }

}
