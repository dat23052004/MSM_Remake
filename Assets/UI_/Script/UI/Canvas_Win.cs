using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Canvas_Win : UICanvas
{
    [SerializeField] TextMeshProUGUI scoreText;

    public void SetBestCoin(int score)
    {
        scoreText.text =score.ToString();
    }
    public void MainMenuButton()
    {
        Close(0);
        UIManager.Ins.OpenUI<Canvas_MainMenu>();
    }
}
