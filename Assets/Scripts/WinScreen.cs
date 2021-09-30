using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinScreen : MonoBehaviour
{
    public Text pointsText;
    public Slider ectsSlider;
    public Slider moneySlider;
    public Slider stressSlider;
    public Player player;
    
    public void SetupWin(int score)
    {
        ectsSlider.gameObject.SetActive(false); 
        moneySlider.gameObject.SetActive(false);
        stressSlider.gameObject.SetActive(false);
        player.audioListener.enabled = false;
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " Credits";
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
