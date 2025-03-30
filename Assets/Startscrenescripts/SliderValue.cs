using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderValue : MonoBehaviour
{
    public void OnSliderValueChanged(float value)
    {
        Debug.Log("Slider value: " + value);
    }

}
