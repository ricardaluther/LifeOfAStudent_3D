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
    private Text _livesText;

    // Start is called before the first frame update
    void Start()
    {
        _livesText.text = "Lives " + _player.GetLives();
    }

    // Update is called once per frame
    void Update()
    {
        _livesText.text = "Lives: " + _player.GetLives();
    }
}
