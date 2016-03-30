using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPS : MonoBehaviour
{
    public Text cont;
    float deltaTime = 0.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        this.deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }
    void OnGUI()
    {
        float fps = 1.0f / this.deltaTime;
        string text2 = string.Format("{0:0.} fps", fps);
        this.cont.text = text2;
    }
}
