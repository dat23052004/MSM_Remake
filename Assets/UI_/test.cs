using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    int score = 0;
    private void Start()
    {
        UIManager.Ins.OpenUI<Canvas_MainMenu>();
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Q) && UIManager.Ins.IsOpened<Canvas_GamePlay>())
        {
            UIManager.Ins.GetUI<Canvas_GamePlay>().UpdateCoin(++score);
        }
        if (Input.GetKeyUp(KeyCode.V) )
        {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Canvas_Win>().SetBestCoin(score);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Canvas_Lose>().SetBestCoin(score);
        }
    }
}
