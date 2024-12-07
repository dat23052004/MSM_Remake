using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HairDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hairBonus;
    [SerializeField] private TextMeshProUGUI hairPrice;
    [SerializeField] private TextMeshProUGUI hairEquip;
    [SerializeField] private Image hairImage;
    [SerializeField] private TextMeshProUGUI coinText;
    //[SerializeField] private Outline equipOutline;
    //[SerializeField] private Outline unequipOutline;

    public void DisplayAndUpdatecoin(HairDataSO hairDataSO, UserData userData, int indexHair)
    {
       
        DisplayHair(hairDataSO,indexHair);
        UpdateCoin(userData);
    }

    public void DisplayHair(HairDataSO hairData, int index)
    {
        hairBonus.text = hairData.listHairs[index].bonus;
        hairPrice.text = hairData.listHairs[index].price.ToString();
        hairImage.sprite = hairData.listHairs[index].image;
    }

    public bool Canchange()
    {
     
        if(hairEquip.text == Constant.UNEQUIP_SKIN)
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
