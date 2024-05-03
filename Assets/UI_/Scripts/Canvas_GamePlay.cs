using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_GamePlay : UICanvas
{
    public void SettingButton()
    {
        
        UIManager.Ins.OpenUI<Canvas_Setting>();
        GameManager.ChangeState(GameState.Setting);
        Time.timeScale = 1;
        Close(0);
    }

    
}
