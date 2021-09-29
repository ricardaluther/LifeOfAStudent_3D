using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stress_Bar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private Boolean _col;

    public void SetStress(int stress)
    {
        slider.value = stress;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (slider.normalizedValue > 0.8)
        {
            if (_col)
            {
                fill.color = Color.red;
            }
            else
            {
                fill.color = Color.black;
            }

            _col = !_col;
        }
    }

    public void SetMaxStress(int stress, int startVal)
    {
        slider.maxValue = stress;
        slider.value = startVal;
        fill.color = gradient.Evaluate(Player.StressStart);
    }
}