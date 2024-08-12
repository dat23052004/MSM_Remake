using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelection : Singleton<WeaponSelection>
{
    [SerializeField] private WeaponDisplay weaponDisplay;
    [SerializeField] private WeaponDataSO weaponDataSO;
    public TextMeshProUGUI currentCoinLeft;
    private UserData data;
    private WeaponType currentWeapShownIndex = (WeaponType)0;
    public Button price;
    public Button unequip;
    public Button equip;

    private void Start()
    {
        data = GameManager.Ins.UserData;
        ShowWeaponAndCoin(currentWeapShownIndex);
    }

    private void Update()
    {
        SetWeaponsAvailability(currentWeapShownIndex);
        CheckEquip();
    }

    public void BuyWeapon()
    {
        if(!data.BoughtWeapons.Contains((int)currentWeapShownIndex) && data.CurrentCoins >= weaponDataSO.listWeapons[(int)currentWeapShownIndex].price)
        {
            data.BoughtWeapons.Add((int)currentWeapShownIndex);
            data.CurrentCoins -= weaponDataSO.listWeapons[(int)currentWeapShownIndex].price;
            currentCoinLeft.SetText(data.CurrentCoins.ToString());
            weaponDisplay.DisplayWeaponAndCoin(weaponDataSO, currentWeapShownIndex, data);
            SaveManager.Ins.SaveData(data);
        }

    }

    public void UseWeapon()
    {
        if (weaponDisplay.CanChange())
        {
            data.EquippedWeapon = (int)currentWeapShownIndex;
            SaveManager.Ins.SaveData(data);
            LevelManager.Ins.player.ChangeWeapon();
        }
    }
    public void NextWeaponInShop()
    {
        if(currentWeapShownIndex == (WeaponType)2)
        {
            currentWeapShownIndex  = (WeaponType)0;
            ShowWeaponAndCoin(currentWeapShownIndex);            
        }
        else
        {
            currentWeapShownIndex++;
            ShowWeaponAndCoin(currentWeapShownIndex);
        }
    }

    public void PrevWeaponInShop()
    {
        if (currentWeapShownIndex == (WeaponType)0)
        {
            currentWeapShownIndex = (WeaponType)2;
            ShowWeaponAndCoin(currentWeapShownIndex);
        }
        else
        {
            currentWeapShownIndex--;
            ShowWeaponAndCoin(currentWeapShownIndex);
        }
    }
    private void ShowWeaponAndCoin(WeaponType weapon)
    {
       
        weaponDisplay.DisplayWeaponAndCoin(weaponDataSO,weapon, data);
    }
    private void SetWeaponsAvailability(WeaponType currentWeaponType)
    {
        if (data.BoughtWeapons.Contains((int)currentWeaponType))
        {
            weaponDisplay.Equiped();
        }
    }
    public void CheckEquip()
    {
        if (!data.BoughtWeapons.Contains((int)currentWeapShownIndex))
        {
            price.gameObject.SetActive(true);
            equip.gameObject.SetActive(false);
            unequip.gameObject.SetActive(false);
        }
        else
        {
            price.gameObject.SetActive(false);
            if (data.EquippedWeapon != (int)currentWeapShownIndex)
            {
                unequip.gameObject.SetActive(true);
                equip.gameObject.SetActive(false);
            }
            else
            {
                unequip.gameObject.SetActive(false);
                equip.gameObject.SetActive(true);
            }
        }
    }
}
