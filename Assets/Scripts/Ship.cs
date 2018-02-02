using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fetches configuration options from the UI.

public class Ship : MonoBehaviour {
    // User-set properties.

    // Pixels per meter. Scales the hull drawing.
    public float scale = 1.5f;

    public Hull hull {
        get {
            return GetComponentInChildren<Hull>();
        }
    }
}
