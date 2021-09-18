using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Ui manager class:
public class UiManager : MonoBehaviour
{
    [SerializeField] 
    private Player _player;
    
    [SerializeField]
    private Text _ectsText;

    [SerializeField] 
    private Text _moneyText;

    [SerializeField] 
    private Text _stressText;

    // Start is called before the first frame update
    void Start()
    {
        _ectsText.text = "ECTS: " + Player.getEcts();
        _moneyText.text = "Money: " + _player.getMoney();
        _stressText.text = "Stress: " + Player.getStress();
    }

    // Update is called once per frame
    void Update()
    {
        _ectsText.text = "ECTS: " + Player.getEcts();
        _moneyText.text = "Money: " + _player.getMoney();
        _stressText.text = "Stress: " + Player.getStress();
    }
}
