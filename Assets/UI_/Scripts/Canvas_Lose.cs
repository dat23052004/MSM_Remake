using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Lose : UICanvas
{
    public void RetryButton()
    {
        UIManager.Ins.OpenUI<Canvas_GamePlay>();
        GameManager.ChangeState(GameState.Gameplay);
        Time.timeScale = 1;
        Close(0);
    }

    public void MenuButton()
    {
        UIManager.Ins.OpenUI<Canvas_MainMenu>();
        GameManager.ChangeState(GameState.MainMenu);
        Time.timeScale = 1;
        Close(0);
    }
}
