using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Setting : UICanvas
{
    public void ContinueButton()
    {
        UIManager.Ins.OpenUI<Canvas_GamePlay>();
        GameManager.ChangeState(GameState.Gameplay);
        Time.timeScale = 1f;
        Close(0);
    }

    public void MenuButton()
    {
        UIManager.Ins.OpenUI<Canvas_MainMenu>();
        GameManager.ChangeState(GameState.MainMenu);
        Time.timeScale = 1f;
        Close(0);
    }
}
