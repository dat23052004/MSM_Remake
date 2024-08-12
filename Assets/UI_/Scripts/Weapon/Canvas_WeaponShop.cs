using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_WeaponShop : UICanvas
{
    public void ButtonNext()
    {

    }
    public void CloseButton()
    {
        UIManager.Ins.OpenUI<Canvas_MainMenu>();
        GameManager.ChangeState(GameState.MainMenu);
        Close(0); 
    }
}
