using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level : MonoBehaviour
{
    
    public float planeWidth;
    public float planeHeight;
    public int totalBotSpawn;
    public int maxBotCount;
    public int levelId;
    public GameObject levelEnvironment;
    public Transform playerSpawnPoint;
}
