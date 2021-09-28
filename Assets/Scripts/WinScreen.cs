using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WinScreen : MonoBehaviour
{
    public Text pointsText;
    
    public void SetupWin(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " Credits";
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
