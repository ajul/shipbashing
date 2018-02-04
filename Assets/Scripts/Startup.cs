using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {

    public HighresCamera highresCamera;

	// Use this for initialization
	void Start () {
        highresCamera.GetComponent<Camera>().Render();
    }
}
