using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Bot botPrefabs;
    [SerializeField] private Player player;
    [SerializeField] private LevelDataSO levelDataSO;
    [NonSerialized] public Level levelPrefabs;
    [NonSerialized] public List<Bot> botList = new List<Bot>();
    public List<Vector3> pointList = new List<Vector3>();
    private int maxBotCount;

    private float width;
    private float height;
    private int totalBotSpawn;
    private Level currentLevel;
    public int IdLevelCurrent;
    public bool finishedLevel = false;

    public GameObject botContainer;

    private void Awake()
    {
        LoadLevel();
        GeneratePoints();
    }

    private void Start()
    {       
        SpawnBotAtStart();
    }

    private void Update()
    {
        CheckAndSpawnMoreBots();
    }

    public void LoadLevel()
    {
        List<Level> levels = levelDataSO.listLevels;
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[IdLevelCurrent]);
        width = currentLevel.planeWidth;
        height = currentLevel.planeHeight;
        totalBotSpawn = currentLevel.totalBotSpawn;
        maxBotCount = currentLevel.maxBotCount;
        player.OnInit();
        player.transform.position = currentLevel.playerSpawnPoint.position;        
    }
    public void GeneratePoints()
    {       
        int numberPoints = 200;
        int i = 0;
        
        while (i < numberPoints)
        {
            float randomWidth = UnityEngine.Random.Range(-width / 2, width / 2);
            float randomDepth = UnityEngine.Random.Range(-height / 2, height / 2);
            float yPosition = 0f;
            Vector3 pointPosition = new Vector3(randomWidth, yPosition, randomDepth);            
            pointList.Add(pointPosition);
            i++;
        }
    }
    

    
    private void SpawnBotAtStart()
    {
        for (int i = 0; i < maxBotCount; i++)
        {
            SpawnBot();
        }
    }
    public void SpawnBot()
    {
        Vector3 spawnpoint = pointList[UnityEngine.Random.Range(0, pointList.Count - 1)];       
        Bot bot = Instantiate(botPrefabs, spawnpoint, Quaternion.identity);
        bot.transform.parent = botContainer.transform;
        totalBotSpawn--;
        botList.Add(bot);
    }

    private void CheckAndSpawnMoreBots()
    {
        //if (botList.Count < maxBotCount && totalBotSpawn != 0)
        //{
        //    SpawnBot();
        //}
        //if (botList.Count == 0)
        //{
        //    Debug.Log("Finished Game!");
        //    finishedLevel = true;
            
        //}
    }
}
