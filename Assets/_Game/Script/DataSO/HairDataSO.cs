using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/HairSO", order = 1)]
public class HairDataSO : ScriptableObject
{
    public List<HairInfo> listHairs;
}
