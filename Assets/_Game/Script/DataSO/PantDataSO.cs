using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/PantSO", order = 1)]
public class PantDataSO : ScriptableObject
{
    public List<PantInfo> listPants;
}
