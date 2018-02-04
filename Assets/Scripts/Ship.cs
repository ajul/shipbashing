using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Fetches configuration options from the UI.

public class Ship : MonoBehaviour {
    public Hull hull {
        get {
            return GetComponentInChildren<Hull>();
        }
    }

    public GameObject turretCountSlider;
    public int turretCount {
        get {
            return (int) turretCountSlider.GetComponentInChildren<Slider>().value;
        }
    }

    public GameObject shellSizeSlider;
    public float shellSize {
        get {
            return shellSizeSlider.GetComponentInChildren<Slider>().value;
        }
    }

    // Pixels per meter. Scales the hull drawing.
    public float scale = 1.0f;

    // How tall each floor is (m).
    public float levelHeight = 2.5f;

    public float superstructureLength {
        get {
            float result = 0.0f;
            Turret turret = new DefaultTurret(shellSize);
            float turretLength = turret.frontClearance + turret.backClearance;
            result += turretCount * turretLength;
            return result;
        }
    }

    public void DrawTurrets() {
        // Temporary.
        Turret turret = new DefaultTurret(shellSize);

        float x = 0.5f * hull.lengthUpperDeck - 0.5f * superstructureLength + turret.backClearance;

        float turretLength = turret.frontClearance + turret.backClearance;
        for (int i = 0; i < turretCount; i++) {
            GL.PushMatrix();
            float y = hull.Height(x);
            GL2.MultMatrix(Matrix4x4.Translate(new Vector3(x, y + levelHeight, 1.0f)));
            turret.Draw(0.5f * y + levelHeight);
            GL.PopMatrix();
            x += turretLength;
        }
    }
}
