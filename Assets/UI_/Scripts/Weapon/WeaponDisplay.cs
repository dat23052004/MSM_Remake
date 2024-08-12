using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponBonus;
    [SerializeField] private TextMeshProUGUI weaponDescription;
    [SerializeField] private TextMeshProUGUI weaponPrice;
    [SerializeField] private TextMeshProUGUI weaponEquip;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Start()
    {
        
    }
    public void DisplayWeaponAndCoin(WeaponDataSO weaponData,WeaponType weaponType, UserData userData)
    {
        ShowWeapon(weaponData,weaponType);
        UpdateCoin(userData);
    }

    private void ShowWeapon(WeaponDataSO weaponData, WeaponType weaponType)
    {
        weaponName.text = weaponData.listWeapons[(int)weaponType].name;
        weaponBonus.text = weaponData.listWeapons[(int)weaponType].bonus;
        weaponDescription.text = weaponData.listWeapons[(int)weaponType].description;
        weaponPrice.text = weaponData.listWeapons[(int)weaponType].price.ToString();
        if (weaponHolder.childCount > 0)
           Destroy(weaponHolder.GetChild(0).gameObject);

        Instantiate(weaponData.listWeapons[(int)weaponType].Model, weaponHolder.position, weaponHolder.rotation, weaponHolder);
    }

    public bool CanChange()
    {
        if (weaponEquip.text == Constant.UNEQUIP_SKIN)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void UpdateCoin(UserData userData)
    {
        coinText.text = userData.CurrentCoins.ToString();
    }

    public void Equiped()
    {
        weaponEquip.SetText(Constant.UNEQUIP_SKIN);
    }

    
}
