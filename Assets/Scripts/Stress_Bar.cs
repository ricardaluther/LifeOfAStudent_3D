using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stress_Bar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;

    public void SetStress(int stress)
    {
        slider.value = stress;
    }

    public void SetMaxStress(int stress, int startVal)
    {
        slider.maxValue = stress;
        slider.value = startVal;
    }
}