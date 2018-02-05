using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBridge : ISuperstructureItem {
    private float _slotSize;
    public float slotSize {
        get { return _slotSize; }
    }

    private readonly float height;

    public DefaultBridge(float length, float height) {
        _slotSize = length;
        this.height = height;
    }

    public void Draw(float depth) {
        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Color(new Color(0.5f, 0.5f, 0.5f));

        GL.Vertex3(0.0f, 0.0f, 1.0f);
        GL.Vertex3(0.0f, -depth, 1.0f);

        GL.Vertex3(slotSize, 0.0f, 1.0f);
        GL.Vertex3(slotSize, -depth, 1.0f);
        GL.End();
    }

}
