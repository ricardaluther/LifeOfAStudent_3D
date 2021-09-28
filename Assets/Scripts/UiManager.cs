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

    public GameOverScreen GameOverScreen;

    public ECTS_Bar ects_bar;
    public Money_Bar moneyBar;
    public Stress_Bar stressBar;

    // Start is called before the first frame update
    void Start()
    {
       // _ectsText.text = "ECTS: " + Player.GetEcts();
        //_moneyText.text = "Money: " + Player.GetMoney();
        //_stressText.text = "Stress: " + Player.GetStress();

        _ectsText.text = "ECTS: ";
        _moneyText.text = "Money: ";
        _stressText.text = "Stress: ";
        
        ects_bar.SetMaxEcts(Player.EctsMax, Player.EctsStart);
        moneyBar.SetMaxMoney(Player.MoneyMax, Player.MoneyStart);
        stressBar.SetMaxStress(Player.StressMax, Player.StressStart);
        
    }

    // Update is called once per frame
    void Update()
    {
       // _ectsText.text = "ECTS " + Player.GetEcts();
        //_moneyText.text = "Money " + Player.GetMoney();
        //_stressText.text = "Stress: " + Player.GetStress();
        
        _ectsText.text = "ECTS: ";
        _moneyText.text = "Money: ";
        _stressText.text = "Stress: ";
        
        ects_bar.SetECTS(Player.GetEcts());
        moneyBar.SetMoney(Player.GetMoney());
        stressBar.SetStress(Player.GetStress());
    }

}
