using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMast : ISuperstructureItem {
    private float _slotSize;
    public float slotSize {
        get { return _slotSize; }
    }

    private readonly float height;

    public DefaultMast(float length, float height) {
        _slotSize = length;
        this.height = height;
    }

    public void Draw(float depth) {
        // Front pole.
        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Color(new Color(0.5f, 0.5f, 0.5f));
        // TODO
        GL.End();
    }

}