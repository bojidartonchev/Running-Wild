using UnityEngine;
using System.Collections;

public class CameraZoomAnimation : MonoBehaviour
{
    public AnimationClip clip;
    private Camera camera;

    // Use this for initialization
    void Start () {
	    this.camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        AnimationCurve curve_fov = AnimationCurve.EaseInOut(0, camera.fieldOfView, 10, 200);
        clip.SetCurve("", typeof(Camera), "field of view", curve_fov);
        
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}