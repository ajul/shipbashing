using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Renders the non-pixel portions of the ship.
public class HighresCamera : MonoBehaviour {

    public Material hullMaterial;
    public Ship ship;

    void OnPostRender() {
        hullMaterial.SetPass(0);

        GL.PushMatrix();
        Matrix4x4 waterlineTransform = Matrix4x4.Translate(new Vector3(0.0f, -25.0f, 0.0f));

        GL2.MultMatrix(waterlineTransform);

        Matrix4x4 hullTransform = Matrix4x4.Translate(new Vector3(-0.5f * ship.hull.lengthOverall, 0.0f, 1.0f));

        GL2.MultMatrix(hullTransform);
        GL2.MultMatrix(Matrix4x4.Scale(new Vector3(ship.scale, ship.scale, ship.scale)));

        ship.DrawTurrets();
        ship.hull.Draw();

        GL.PopMatrix();
    }

    private void DrawSuperstructure() {
        // Testing.
        
    }
}
