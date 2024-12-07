using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Canvas_ItemShop : UICanvas
{
    public GameObject scrollView1;
    public GameObject scrollView2;
    public GameObject scrollView3;
    public GameObject scrollView4;

    private void Start()
    {
        ActivateScrollView(scrollView1);
        DeactivateOtherScrollViews(scrollView1);
    }
    public void CloseButton()
    {
        UIManager.Ins.OpenUI<Canvas_MainMenu>();
        GameManager.ChangeState(GameState.MainMenu);
        LevelManager.Ins.player.ChangeHair();
        Time.timeScale = 1f;
        Close(0);
    }

    public void OnButtonHair()
    {
        ActivateScrollView(scrollView1);
        DeactivateOtherScrollViews(scrollView1);
    }
    public void OnbuttonPant()
    {
        ActivateScrollView(scrollView2);
        DeactivateOtherScrollViews(scrollView2);
    }

    private void ActivateScrollView(GameObject scrollView)
    {
        scrollView.SetActive(true);
    }

    private void DeactivateOtherScrollViews(GameObject activeScrollView)
    {
        GameObject[] allScrollViews = { scrollView1, scrollView2, scrollView3, scrollView4 };

        foreach (GameObject scrollView in allScrollViews)
        {
            if (scrollView != activeScrollView)
            {
                scrollView.SetActive(false);
            }
        }
    }
}
