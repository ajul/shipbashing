using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret {
    public abstract float shellSize {
        get;
    }

    public abstract float frontClearance {
        get;
    }

    public abstract float backClearance {
        get;
    }

    // Draw the turret at the current position, atop a barbette of the specified depth.
    // Origin is at the bottom center of the gunhouse.
    public abstract void Draw(float barbetteDepth);
}

public class DefaultTurret : Turret {
    // In mm.
    public readonly float _shellSize;
    public override float shellSize {
        get { return _shellSize; }
    }

    private const float mmPerInch = 25.4f;

    private const float zBarbette = 0.25f;
    private const float zGunhouse = 0.0f;
    private const float zBarrel = 0.5f;

    // Length of barrel in calibers.
    private const float calibers = 40.0f;
    private float barrelLength {
        get { return 0.001f * shellSize * calibers; }
    }

    private float barrelDiameterTipMult = 2.0f;
    private float barrelSlope = 0.05f;
    private float barrelDiameterTip {
        get { return 0.001f * shellSize * barrelDiameterTipMult; }
    }
    private float barrelDiameterRoot {
        get { return barrelDiameterTip + barrelSlope * barrelLength; }
    }
    
    // Gunhouse length as a fraction of barrel length.
    private const float gunhouseLengthFraction = 1.0f;
    private const float minGunhouseLength = 3.0f;
    private float gunhouseLength {
        get {
            return Mathf.Max(minGunhouseLength, barrelLength * gunhouseLengthFraction);
        }
    }

    private const float gunhouseHeight = 3.0f;

    // Barbette length as a fraction of gunhouse length.
    private const float barbetteLengthFraction = 0.75f;
    private float barbetteLength {
        get {
            return gunhouseLength * barbetteLengthFraction;
        }
    }

    public override float frontClearance {
        get {
            return barrelLength + 0.5f * barbetteLength;
        }
    }

    public override float backClearance {
        get {
            return gunhouseLength + gunhouseHeight - 0.5f * barbetteLength;
        }
    }

    public DefaultTurret(float shellSize, bool inInches = false) {
        if (inInches) {
            _shellSize = shellSize * mmPerInch;
        } else {
            _shellSize = shellSize;
        }
    }

    public override void Draw(float barbetteDepth) {
        GL.PushMatrix();

        Matrix4x4 frontOfBarbette = Matrix4x4.Translate(new Vector3(0.5f * barbetteLength, 0.0f, 0.0f));
        GL2.MultMatrix(frontOfBarbette);

        // Barrel.

        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Color(new Color(0.5f, 0.5f, 0.5f));

        float barrelBottom = 0.5f * gunhouseHeight - 0.5f * barrelDiameterRoot;

        GL.Vertex3(-gunhouseHeight, barrelBottom + barrelDiameterRoot, zBarrel);
        GL.Vertex3(-gunhouseHeight, barrelBottom, zBarrel);

        GL.Vertex3(-gunhouseHeight + barrelLength, barrelBottom + barrelDiameterTip, zBarrel);
        GL.Vertex3(-gunhouseHeight + barrelLength, barrelBottom, zBarrel);
        GL.End();

        // Barbette.

        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Color(new Color(0.125f, 0.125f, 0.125f));

        GL.Vertex3(-barbetteLength, 0.0f, zBarbette);
        GL.Vertex3(-barbetteLength, -barbetteDepth, zBarbette);

        GL.Vertex3(0.0f, 0.0f, zBarbette);
        GL.Vertex3(0.0f, -barbetteDepth, zBarbette);

        GL.End();

        // Gunhouse.

        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Color(new Color(0.25f, 0.25f, 0.25f));
        GL.Vertex3(-gunhouseLength, gunhouseHeight, zGunhouse);
        GL.Vertex3(-gunhouseLength, 0.0f, zGunhouse);
        GL.Vertex3(-gunhouseHeight, gunhouseHeight, zGunhouse);
        GL.Vertex3(0.0f, 0.0f, zGunhouse);
        GL.End();

        GL.PopMatrix();
    }
}