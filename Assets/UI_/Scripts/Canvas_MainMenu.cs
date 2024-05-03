using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_MainMenu : UICanvas
{
    public void GamePlayButton()
    {
        UIManager.Ins.OpenUI<Canvas_GamePlay>();
        GameManager.ChangeState(GameState.Gameplay);
        Time.timeScale = 1f;
        Close(0);
    }


}
