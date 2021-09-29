using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money_Bar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    private Boolean _col;

    public void SetMoney(int money)
    {
        slider.value = money;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        if (slider.normalizedValue < 0.2)
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

    public void SetMaxMoney(int money, int startVal)
    {
        slider.maxValue = money;
        slider.value = startVal;
        fill.color = gradient.Evaluate(Player.MoneyStart);
    }
}