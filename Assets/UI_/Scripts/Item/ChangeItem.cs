using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeItem : Singleton<ChangeItem>
{
    [SerializeField] private HairDataSO hairDataSO;
    [SerializeField] private HairDisplay[] hairDisplays;

    [SerializeField] private PantDataSO pantDataSO;
    [SerializeField] private PantDisplay[] pantDisplays;

    public TextMeshProUGUI currentCointLeft;
    private UserData data;
    private HairType currentHairShowIndex = (HairType)0;
    private PantType currentPantShowIndex = (PantType)0;

    public int currentTypeItem;
    public Button price;
    public Button unequip;
    public Button equip;

    private void Awake()
    {
        data = GameManager.Ins.UserData;
        Display_Item();
    }

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        CheckEquip();

    }

    public void OnInit()
    {
        currentTypeItem = 0;
    }
    public void BuyItem()
    {
        Debug.Log("mua mua mua");
        int currentHair = (int)currentHairShowIndex;
        int currentPant = (int)currentPantShowIndex;
        if (!data.BoughtHats.Contains(currentHair) && data.CurrentCoins >= hairDataSO.listHairs[currentHair].price)
        {
            data.BoughtHats.Add(currentHair);
            data.CurrentCoins -= hairDataSO.listHairs[currentHair].price;
            data.CurrentCoins -= hairDataSO.listHairs[currentHair].price;
            currentCointLeft.SetText(data.CurrentCoins.ToString());
            SaveManager.Ins.SaveData(data);
            //hairDisplays[currentHair].DisplayAndUpdatecoin(hairDataSO, data, currentHair);
        }


        if (!data.BoughtPants.Contains(currentPant) && data.CurrentCoins >= pantDataSO.listPants[currentPant].price)
        {
            data.BoughtPants.Add(currentPant);
            data.CurrentCoins -= pantDataSO.listPants[currentPant].price;
            data.CurrentCoins -= pantDataSO.listPants[currentPant].price;
            currentCointLeft.SetText(data.CurrentCoins.ToString());
            SaveManager.Ins.SaveData(data);
            //hairDisplays[currentHair].DisplayAndUpdatecoin(hairDataSO, data, currentHair);
        }
    }

    public void UseItem()
    {
        if (currentTypeItem == 0)
        {
            if (hairDisplays[(int)currentHairShowIndex].Canchange())
            {
                data.EquippedHat = (int)currentHairShowIndex;
                Debug.Log((int)currentHairShowIndex);
                SaveManager.Ins.SaveData(data);
                LevelManager.Ins.player.ChangeHair();
            }
        }
        if (currentTypeItem == 1)
        {
            if (pantDisplays[(int)currentPantShowIndex].CanChange())
            {
                data.EquippedPant = (int)currentPantShowIndex;
                SaveManager.Ins.SaveData(data);
                LevelManager.Ins.player.ChangePant();
            }
        }
    }

    public void CheckEquip()
    {
        if (currentTypeItem == 0)
        {
            if (!data.BoughtHats.Contains((int)currentHairShowIndex))
            {
                price.gameObject.SetActive(true);
                equip.gameObject.SetActive(false);
                unequip.gameObject.SetActive(false);
            }
            else
            {
                price.gameObject.SetActive(false);
                if (data.EquippedHat != (int)currentHairShowIndex)
                {
                    equip.gameObject.SetActive(false);
                    unequip.gameObject.SetActive(true);
                }
                else
                {
                    equip.gameObject.SetActive(true);
                    unequip.gameObject.SetActive(false);
                }
            }
        }

        if(currentTypeItem == 1)
        {
            if (!data.BoughtPants.Contains((int)currentPantShowIndex))
            {
                price.gameObject.SetActive(true);
                equip.gameObject.SetActive(false);
                unequip.gameObject.SetActive(false);
            }
            else
            {
                price.gameObject.SetActive(false);
                if (data.EquippedPant != (int)currentPantShowIndex)
                {
                    equip.gameObject.SetActive(false);
                    unequip.gameObject.SetActive(true);
                }
                else
                {
                    equip.gameObject.SetActive(true);
                    unequip.gameObject.SetActive(false);
                }
            }
        }
    }

    private void ShowDisplayAndCoin_Hair(HairType hairType, int index)
    {
        hairDisplays[(int)hairType].DisplayAndUpdatecoin(hairDataSO, data, index);
    }

    public void ButtonHair(int index)
    {
        if (index >= 0 && index < hairDisplays.Length)
        {
            currentHairShowIndex = (HairType)index;
            ShowDisplayAndCoin_Hair(currentHairShowIndex, index);
            currentTypeItem = 0;
            LevelManager.Ins.player.TryHair(index);
        }
        else
        {
            Debug.LogWarning("Invalid button index: " + index);
        }


    }
    public void Display_Item()
    {
        for (int i = 0; i < hairDisplays.Length; i++)
        {
            hairDisplays[i].DisplayHair(hairDataSO, i);
        }

        for (int i = 0; i < pantDisplays.Length; i++)
        {
            pantDisplays[i].DisplayPant(pantDataSO, i);
        }

    }

    private void ShowDisplayAndCoin_Pant(PantType pantType, int index)
    {
        pantDisplays[(int)pantType].DisplayAndUpdatecoin(pantDataSO, data, index);
    }

    public void ButtonPant(int index)
    {
        if (index >= 0 && index < pantDisplays.Length)
        {
            currentPantShowIndex = (PantType)index;
            Debug.Log(currentPantShowIndex.ToString());
            ShowDisplayAndCoin_Pant(currentPantShowIndex, index);
            currentTypeItem = 1;
            LevelManager.Ins.player.TryPant(index);
        }
        else
        {
            Debug.LogWarning("Invalid button index: " + index);
        }
    }

}
