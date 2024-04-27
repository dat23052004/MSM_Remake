using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Canvas_GamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI coinText;

    public override void Setup()
    {
        base.Setup();
        UpdateCoin(0);
    }
    public void UpdateCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Canvas_Setting>().SetState(this);
    }
}
