using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/WeaponSO", order = 1)]
public class WeaponDataSO : ScriptableObject
{
    public List<WeaponInfo> listWeapons;
}
