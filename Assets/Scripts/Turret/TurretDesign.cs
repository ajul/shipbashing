using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Turret {

    public abstract class Design {
        public abstract float shellSize {
            get;
        }

        public abstract float slotSizeNormal {
            get;
        }

        // How much length this turret takes up if it is allowed to superfire.
        public abstract float slotSizeSuperfiring {
            get;
        }

        public abstract float superfiringHeight {
            get;
        }

        // Draw the turret at the current position.
        // Origin is at the bottom aft of the slot, at the bottom of the gunhouse.
        public abstract void Draw(float barbetteDepth);
    }

    public class DefaultDesign : Design {
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
        private const float minGunhouseLength = 5.0f;
        private float gunhouseLength {
            get {
                return Mathf.Max(minGunhouseLength, barrelLength * gunhouseLengthFraction);
            }
        }

        private const float gunhouseHeight = 3.0f;

        // How much space to leave in front/behind slot.
        private const float slotMargin = 0.5f;

        public override float superfiringHeight {
            get {
                if (shellSize >= 140.0f) {
                    return gunhouseHeight;
                } else {
                    return gunhouseHeight * 0.5f;
                }
            }
        }

        // Barbette length as a fraction of gunhouse length.
        private const float barbetteLengthFraction = 0.75f;
        private float barbetteLength {
            get {
                return gunhouseLength * barbetteLengthFraction;
            }
        }

        public override float slotSizeNormal {
            get {
                return gunhouseLength - gunhouseHeight + barrelLength + 2.0f * slotMargin;
            }
        }

        public override float slotSizeSuperfiring {
            get {
                return gunhouseLength + 2.0f * slotMargin;
            }
        }

        public DefaultDesign(float shellSize, bool inInches = false) {
            if (inInches) {
                _shellSize = shellSize * mmPerInch;
            } else {
                _shellSize = shellSize;
            }
        }

        public override void Draw(float barbetteDepth) {
            // Barrel.

            GL.Begin(GL.TRIANGLE_STRIP);
            GL.Color(new Color(0.5f, 0.5f, 0.5f));

            float barrelBottom = 0.5f * gunhouseHeight - 0.5f * barrelDiameterRoot;

            float xBarrelStart = slotMargin + gunhouseLength - gunhouseHeight;
            GL.Vertex3(xBarrelStart, barrelBottom + barrelDiameterRoot, zBarrel);
            GL.Vertex3(xBarrelStart, barrelBottom, zBarrel);

            GL.Vertex3(xBarrelStart + barrelLength, barrelBottom + barrelDiameterTip, zBarrel);
            GL.Vertex3(xBarrelStart + barrelLength, barrelBottom, zBarrel);
            GL.End();

            // Barbette.

            GL.Begin(GL.TRIANGLE_STRIP);
            GL.Color(new Color(0.125f, 0.125f, 0.125f));

            float xBarbetteEnd= slotMargin + gunhouseLength;
            GL.Vertex3(xBarbetteEnd - barbetteLength, 0.0f, zBarbette);
            GL.Vertex3(xBarbetteEnd - barbetteLength, -barbetteDepth, zBarbette);

            GL.Vertex3(xBarbetteEnd, 0.0f, zBarbette);
            GL.Vertex3(xBarbetteEnd, -barbetteDepth, zBarbette);

            GL.End();

            // Gunhouse.

            GL.Begin(GL.TRIANGLE_STRIP);
            GL.Color(new Color(0.25f, 0.25f, 0.25f));
            float xGunhouseStart = slotMargin;
            GL.Vertex3(xGunhouseStart, gunhouseHeight, zGunhouse);
            GL.Vertex3(xGunhouseStart, 0.0f, zGunhouse);
            GL.Vertex3(xGunhouseStart + gunhouseLength - gunhouseHeight, gunhouseHeight, zGunhouse);
            GL.Vertex3(xGunhouseStart + gunhouseLength, 0.0f, zGunhouse);
            GL.End();
        }
    }

}