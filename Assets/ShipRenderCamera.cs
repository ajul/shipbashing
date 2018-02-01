using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRenderCamera : MonoBehaviour {

    public Material hullMaterial;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnPostRender() {

        hullMaterial.SetPass(0);

        GL.Begin(GL.TRIANGLES);

        GL.Color(new Color(1.0f, 0.0f, 0.0f, 2.0f));
        GL.Vertex3(0.0f, 0.0f, 1.0f);

        GL.Color(new Color(0.0f, 1.0f, 0.0f, 2.0f));
        GL.Vertex3(50.0f, 0.0f, 1.0f);

        GL.Color(new Color(0.0f, 0.0f, 1.0f, 2.0f));
        GL.Vertex3(0.0f, 50.0f, 1.0f);

        GL.End();

        
    }
}
