using UnityEngine;
using System.Collections;

public class CutsceneShipHandler : MonoBehaviour
{

    public GameObject explosion;
    public GameObject secondExplosion;

    public AudioClip smallBomb;
    public AudioClip bigBomb;

    private RectTransform ship;
    // Use this for initialization
    void Start()
    {
        this.ship = GetComponent<RectTransform>();
        InvokeRepeating("MoveTheShip", 0, 0.1f);
        Invoke("ExecuteBackExplosion",10f);

    }

    // Update is called once per frame
    void MoveTheShip()
    {
        ship.localScale = new Vector3(ship.localScale.x + 0.005f, ship.localScale.y + 0.005f, ship.localScale.z + 0.005f);
        ship.localPosition = new Vector3(ship.localPosition.x - 0.5f, ship.localPosition.y - 0.5f, ship.localPosition.z);
    }

    void ExecuteBackExplosion()
    {
        AudioSource.PlayClipAtPoint(smallBomb, new Vector3(222.4f, 138f, -210f),0.8f);
        secondExplosion.SetActive(true);
        Invoke("ExecuteFrontExplosion",2f);
    }

    void ExecuteFrontExplosion()
    {
        AudioSource.PlayClipAtPoint(bigBomb, new Vector3(222.4f, 138f, -210f),0.8f);
        explosion.SetActive(true);
    }

    void DestroyShip()
    {
        Destroy(this);
    }
}
