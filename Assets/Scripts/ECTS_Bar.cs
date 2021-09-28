using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ECTS_Bar : MonoBehaviour
{

    public Slider slider;

    public void SetECTS(int ects)
    {
        slider.value = ects;
    }

    public void SetMaxEcts(int ects, int startVal)
    {
        slider.maxValue = ects;
        slider.value = startVal;
    }
}
