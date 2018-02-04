using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Funnel {
    public abstract float length {
        get;
    }

    public abstract void Draw(float bottom, float top);
}

// Straight funnel.
public class DefaultFunnel : Funnel {
    private float _length;
    public override float length {
        get { return _length; }
    }

    public DefaultFunnel(float length) {
        _length = length;
    }

    public override void Draw(float bottom, float top) {
        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Color(new Color(0.5f, 0.5f, 0.5f));

        GL.Vertex3(0.0f, top, 1.0f);
        GL.Vertex3(0.0f, bottom, 1.0f);

        GL.Vertex3(length, top, 1.0f);
        GL.Vertex3(length, bottom, 1.0f);
        GL.End();
    }
}