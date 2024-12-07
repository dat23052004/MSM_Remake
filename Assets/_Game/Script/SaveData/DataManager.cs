using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : Singleton<DataManager>
{
    public WeaponDataSO wpDataSO;
    public HairDataSO hairDataSO;
    public PantDataSO pantDataSO;

    public WeaponInfo GetWeaponData(int weaponIndex)
    {
        //if (wpDataSO == null || wpDataSO.listWeapons == null || weaponIndex < 0 || weaponIndex >= wpDataSO.listWeapons.Count)
        //{
        //    return null;
        //}
        return wpDataSO.listWeapons[weaponIndex];
    }

    public HairInfo GetHatData(int HairData)
    {
        return hairDataSO.listHairs[HairData];
    }

    public PantInfo GetPantData(int PantIndex)
    {
        return pantDataSO.listPants[PantIndex];

    }
}
