using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money_Bar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;

    public void SetMoney(int money)
    {
        slider.value = money;
    }

    public void SetMaxMoney(int money, int startVal)
    {
        slider.maxValue = money;
        slider.value = startVal;
    }
}