using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_MainMenu : UICanvas
{
    public void PlayButton()
    {
        Close(0);
        UIManager.Ins.OpenUI<Canvas_GamePlay>();
    }

    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Canvas_Setting>().SetState(this);
    }
}
