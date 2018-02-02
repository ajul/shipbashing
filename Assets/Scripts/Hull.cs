using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hull : MonoBehaviour {

    public float breakAspectRatio = 1.5f;

    // Length measured by the highest deck of the ship.
    public GameObject lengthUpperDeckSlider;
    public float lengthUpperDeck {
        get {
            return lengthUpperDeckSlider.GetComponentInChildren<Slider>().value;
        }
    }

    // Height above waterline at lowest point (not counting any break).
    public GameObject freeboardSlider;
    public float freeboard {
        get {
            return freeboardSlider.GetComponentInChildren<Slider>().value;
        }
    }

    // Separates the foredeck (or a cut-down quarterdeck) from the rest of the hull.
    public float breakFraction = 0.5f;
    public float breakHeight = 0.0f;

    // Must be positive.
    public float sheerForward = 2.0f;
    public float sheerAft = 1.0f;

    // Expressed as a slope.
    // Positive = longer at top.
    public float stemRake = 0.5f;
    public float sternRake = 0.25f;

    // Derived properties.

    public float bowHeight {
        get { return freeboard + breakHeight + sheerForward; }
    }

    public float sternHeight {
        get { return freeboard + sheerAft; }
    }

    public float stemHang {
        get { return bowHeight * stemRake; }
    }

    public float sternHang {
        get { return sternHeight * sternRake; }
    }

    public float lengthOverall {
        get { return lengthUpperDeck + Mathf.Max(0.0f, -stemHang) + Mathf.Max(0.0f, -sternHang); }
    }

    public float lengthWaterline {
        get { return lengthUpperDeck - stemHang - sternHang; }
    }

    // x is measured according to length overall, starting from the stern.
    public float Height(float x) {
        float result = freeboard;

        float fraction = x / lengthOverall;

        // Parabolic sheer.
        float sheerTotal = sheerForward + sheerAft;
        float sheerFraction = sheerAft / sheerTotal;

        if (fraction < sheerFraction) {
            float a = (sheerFraction - fraction) / sheerFraction;
            result += sheerAft * a * a;
        } else {
            float a = (fraction - sheerFraction) / (1.0f - sheerFraction);
            result += sheerForward * a * a;
        }

        // Break.
        float breakParameter = (breakFraction * lengthUpperDeck - x) / breakAspectRatio;
        if (breakParameter <= 0.0f) {
            // In front of break.
            result += breakHeight;
        } else if (breakParameter < breakHeight && breakParameter < 1.0f) {
            // Close behind break.
            result += breakHeight * breakParameter * breakParameter;
        }

        return result;
    }
}
