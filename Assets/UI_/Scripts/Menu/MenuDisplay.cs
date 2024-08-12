using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private UserData userData;

    private void Start()
    {
        userData = GameManager.Ins.UserData;
        UpdateCoint();
    }
    
    private void UpdateCoint()
    {
        coinText.text = userData.CurrentCoins.ToString();
    }
}
