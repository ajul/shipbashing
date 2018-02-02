using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fetches configuration options from the UI.

public class ShipConfiguration : MonoBehaviour {

    public float scale {
        get {
            return 1.5f;
        }
    }

    public int hullLength {
        get {
            return 150;
        }
    }

    public float HullHeight(float x) {
        return 6.0f;
    }
}
