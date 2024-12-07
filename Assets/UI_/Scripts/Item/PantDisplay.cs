using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PantDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pantPrice;
    [SerializeField] private TextMeshProUGUI pantBonus;
    [SerializeField] private TextMeshProUGUI pantEquip;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Image pantImage;

    public void DisplayAndUpdatecoin(PantDataSO pantDataSO, UserData userData, int indexPant)
    {
        Debug.Log("display");
        DisplayPant(pantDataSO, indexPant);
        UpdateCoin(userData);
    }

    public void DisplayPant(PantDataSO pantData, int index)
    {
        pantBonus.text = pantData.listPants[index].bonus;
        pantPrice.text = pantData.listPants[index].price.ToString();
        pantImage.sprite = pantData.listPants[index].image;
    }
    public bool CanChange()
    {
        if (pantEquip.text == Constant.UNEQUIP_SKIN)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void UpdateCoin(UserData userData)
    {
        coinText.text = userData.CurrentCoins.ToString();
    }
    
}
