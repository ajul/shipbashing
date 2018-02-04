using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretDesign {
    public abstract float shellSize {
        get;
    }

    public abstract float slotSize {
        get;
    }

    // How much length this turret takes up if it is allowed to superfire.
    public abstract float slotSizeSuperfiring {
        get;
    }

    public abstract float barbetteDepth {
        get;
        set;
    }

    // Draw the turret at the current position.
    // Origin is at the bottom aft of the slot.
    public abstract void Draw();
}

public class DefaultTurretDesign : TurretDesign {
    // In mm.
    public readonly float _shellSize;
    public override float shellSize {
        get { return _shellSize; }
    }

    private float _barbetteDepth;
    public override float barbetteDepth {
        get {
            return _barbetteDepth;
        }

        set {
            _barbetteDepth = value;
        }
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
    private const float minGunhouseLength = 5.0f;
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

    public override float slotSize {
        get {
            return gunhouseLength + gunhouseHeight + barrelLength;
        }
    }

    public override float slotSizeSuperfiring {
        get {
            return gunhouseLength + gunhouseHeight;
        }
    }

    public DefaultTurretDesign(float shellSize, bool inInches = false) {
        if (inInches) {
            _shellSize = shellSize * mmPerInch;
        } else {
            _shellSize = shellSize;
        }
    }

    public override void Draw() {
        // Barrel.

        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Color(new Color(0.5f, 0.5f, 0.5f));

        float barrelBottom = 0.5f * gunhouseHeight - 0.5f * barrelDiameterRoot;

        GL.Vertex3(gunhouseLength, barrelBottom + barrelDiameterRoot, zBarrel);
        GL.Vertex3(gunhouseLength, barrelBottom, zBarrel);

        GL.Vertex3(gunhouseLength + barrelLength, barrelBottom + barrelDiameterTip, zBarrel);
        GL.Vertex3(gunhouseLength + barrelLength, barrelBottom, zBarrel);
        GL.End();

        // Barbette.

        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Color(new Color(0.125f, 0.125f, 0.125f));

        GL.Vertex3(gunhouseHeight + gunhouseLength - barbetteLength, 0.0f, zBarbette);
        GL.Vertex3(gunhouseHeight + gunhouseLength - barbetteLength, -barbetteDepth, zBarbette);

        GL.Vertex3(gunhouseHeight + gunhouseLength, 0.0f, zBarbette);
        GL.Vertex3(gunhouseHeight + gunhouseLength, -barbetteDepth, zBarbette);

        GL.End();

        // Gunhouse.

        GL.Begin(GL.TRIANGLE_STRIP);
        GL.Color(new Color(0.25f, 0.25f, 0.25f));
        GL.Vertex3(gunhouseHeight, gunhouseHeight, zGunhouse);
        GL.Vertex3(gunhouseHeight, 0.0f, zGunhouse);
        GL.Vertex3(gunhouseLength, gunhouseHeight, zGunhouse);
        GL.Vertex3(gunhouseHeight + gunhouseLength, 0.0f, zGunhouse);
        GL.End();
    }
}