using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, ShopWeapon, ShopItem, Gameplay, Setting, Pause, Win, Lose }

public class GameManager : Singleton<GameManager>
{   
    private static GameState gameState = GameState.MainMenu;
    public UserData UserData;

    protected void Awake()
    {

        if (SaveManager.Ins.HasData<UserData>())
        {
            Debug.Log("Load Data");
            UserData = SaveManager.Ins.LoadData<UserData>();
        }
        else
        {
            Debug.Log("Created Data");
            UserData = new UserData();
            SaveManager.Ins.SaveData(UserData);
        }

        UIManager.Ins.OpenUI<Canvas_MainMenu>();
        ChangeState(GameState.MainMenu);

        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
        }
    }

    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state)
    {
        return gameState == state;
    }
}
