using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullRenderCamera : MonoBehaviour {

    public Material hullMaterial;
    public Ship ship;

    private readonly float zHull = 1.0f;
    private readonly int hullSections = 100;

    void OnPostRender() {

        hullMaterial.SetPass(0);

        GL.PushMatrix();
        Matrix4x4 waterlineTransform = Matrix4x4.Translate(new Vector3(0.0f, -25.0f, 0.0f));

        GL2.MultMatrix(waterlineTransform);

        GL2.MultMatrix(Matrix4x4.Scale(new Vector3(ship.scale, ship.scale, ship.scale)));

        DrawHull();
        DrawSuperstructure();

        GL.PopMatrix();
    }

    private void DrawHull() {
        GL.PushMatrix();

        Matrix4x4 hullTransform = Matrix4x4.Translate(new Vector3(-0.5f * ship.hull.lengthOverall, 0.0f, 0.0f));

        GL2.MultMatrix(hullTransform);

        GL.Begin(GL.TRIANGLE_STRIP);

        GL.Color(new Color(0.0f, 0.0f, 0.0f, 1.0f));

        for (int i = 0; i < hullSections + 1; i++) {
            float fraction = (float) i / hullSections;
            float xUpper = fraction * ship.hull.lengthUpperDeck;
            float xLower = fraction * ship.hull.lengthWaterline + ship.hull.sternHang;

            float y = ship.hull.Height(xUpper);

            GL.Vertex3(xUpper, y, zHull);
            GL.Vertex3(xLower, 0.0f, zHull);
        }

        GL.End();

        GL.PopMatrix();
    }

    private void DrawSuperstructure() {
        
    }
}
