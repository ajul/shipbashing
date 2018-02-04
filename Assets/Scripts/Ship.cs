using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Fetches configuration options from the UI.

public class Ship : MonoBehaviour {
    // Pixels per meter. Scales the hull drawing.
    public float scale = 1.0f;

    // How tall each floor is (m).
    public float levelHeight = 2.5f;

    public Hull hull {
        get {
            return GetComponentInChildren<Hull>();
        }
    }

    public Superstructure superstructure {
        get {
            return GetComponentInChildren<Superstructure>();
        }
    }

    // Main battery.

    public GameObject mainBatteryTurretCountSlider;
    public int mainBatteryTurretCount {
        get {
            return (int) mainBatteryTurretCountSlider.GetComponentInChildren<Slider>().value;
        }
    }

    public GameObject mainBatteryShellSizeSlider;
    public float mainBatteryShellSize {
        get {
            return mainBatteryShellSizeSlider.GetComponentInChildren<Slider>().value;
        }
    }

    private Turret.Design mainBatteryTurretDesign {
        get {
            return new Turret.DefaultDesign(mainBatteryShellSize);
        }
    }

    public GameObject mainBatteryConfigurationSelector;

    private Turret.Configuration mainBatteryTurretConfiguration {
        get {
            Dropdown dropdown = mainBatteryConfigurationSelector.GetComponentInChildren<Dropdown>();
            int index = dropdown.value;
            string text = dropdown.options[index].text;
            string name = text.Split(' ')[0];
            return Turret.Configuration.TurretConfigurationFromName(name);
        }
    }

    public float topStructureLength {
        get {
            float result = 0.0f;
            result += mainBatteryTurretConfiguration.TotalLength(mainBatteryTurretDesign, mainBatteryTurretCount);
            return result;
        }
    }

    public void UpdateMaximumMainBatteryTurretCount() {
        mainBatteryTurretCountSlider.GetComponentInChildren<Slider>().maxValue = mainBatteryTurretConfiguration.placementCount;
    }

    public void DrawTurrets() {
        float x = 0.5f * hull.lengthUpperDeck - 0.5f * topStructureLength;

        foreach (Turret.Placement placement in mainBatteryTurretConfiguration.GetEnumeratorN(mainBatteryTurretCount)) {
            GL.PushMatrix();
            float y = hull.Height(x) + placement.level * mainBatteryTurretDesign.superfiringHeight;

            if (placement.facesForwards) {
                GL2.MultMatrix(Matrix4x4.Translate(new Vector3(x, y, 1.0f)));
            } else {
                GL2.MultMatrix(Matrix4x4.Translate(new Vector3(x + placement.SlotSize(mainBatteryTurretDesign), y, 1.0f)));
                GL2.MultMatrix(Matrix4x4.Scale(new Vector3(-1.0f, 1.0f, 1.0f)));
            }

            mainBatteryTurretDesign.Draw(y - 0.5f * hull.Height(x));
            GL.PopMatrix();
            x += placement.SlotSize(mainBatteryTurretDesign);
        }
    }
}
