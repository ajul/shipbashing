using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullRenderCamera : MonoBehaviour {

    public Material hullMaterial;
    public ShipConfiguration shipConfiguration;

    private readonly float zHull = 1.0f;

    void OnPostRender() {

        hullMaterial.SetPass(0);

        GL.PushMatrix();
        Matrix4x4 waterlineTransform = Matrix4x4.Translate(new Vector3(0.0f, -25.0f, 0.0f));

        GL2.MultMatrix(waterlineTransform);

        GL2.MultMatrix(Matrix4x4.Scale(new Vector3(shipConfiguration.scale, shipConfiguration.scale, shipConfiguration.scale)));

        DrawHull();
        DrawSuperstructure();

        GL.PopMatrix();
    }

    private void DrawHull() {
        GL.PushMatrix();

        Matrix4x4 hullTransform = Matrix4x4.Translate(new Vector3(-0.5f * shipConfiguration.hullLength, 0.0f, 0.0f));

        GL2.MultMatrix(hullTransform);

        GL.Begin(GL.TRIANGLE_STRIP);

        GL.Color(new Color(0.0f, 0.0f, 0.0f, 1.0f));

        float hullHeight = shipConfiguration.HullHeight(0.0f);

        GL.Vertex3(0.0f, hullHeight, zHull);
        GL.Vertex3(0.0f, 0.0f, zHull);

        for (int i = 0; i < shipConfiguration.hullLength; i++) {
            float x = i + 1.0f;
            hullHeight = shipConfiguration.HullHeight(x);

            GL.Vertex3(x, hullHeight, zHull);
            GL.Vertex3(x, 0.0f, zHull);
        }

        GL.End();

        GL.PopMatrix();
    }

    private void DrawSuperstructure() {
        
    }
}
