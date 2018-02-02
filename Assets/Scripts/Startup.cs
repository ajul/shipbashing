using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {

    public HullRenderCamera hullRenderCamera;

	// Use this for initialization
	void Start () {
        hullRenderCamera.GetComponent<Camera>().Render();
    }
}
