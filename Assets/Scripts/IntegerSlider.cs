using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntegerSlider : MonoBehaviour {

    private string formatString;

    void Start () {
        formatString = GetComponentInChildren<Text>().text;
    }
	
	// Update is called once per frame
	void Update () {
        int value = (int) GetComponentInChildren<Slider>().value;
        GetComponentInChildren<Text>().text = string.Format(formatString, value);
    }
}
