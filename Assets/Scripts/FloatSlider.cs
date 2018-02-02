using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatSlider : MonoBehaviour {

    private string formatString;

    void Start() {
        formatString = GetComponentInChildren<Text>().text;
    }

    // Update is called once per frame
    void Update() {
        float value = GetComponentInChildren<Slider>().value;
        GetComponentInChildren<Text>().text = string.Format(formatString, value);
    }
}
