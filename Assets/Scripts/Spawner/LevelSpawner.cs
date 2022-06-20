using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject[] backgroundPrefabs;
    [SerializeField] private GameObject[] gatePrefabs;
    private BackgroundSpawnPoint[] backgroundSpawnPoints;
    private GateSpawnPoint[] gateSpawnPoints;
    private ObstacleSpawnPoint[] obstacleSpawnPoints;
    private void Start()
    {
        obstacleSpawnPoints = GetComponentsInChildren<ObstacleSpawnPoint>();
        gateSpawnPoints = GetComponentsInChildren<GateSpawnPoint>();
        backgroundSpawnPoints = GetComponentsInChildren<BackgroundSpawnPoint>();
        GenerateObstacle();
        GenerateGate();
        GenerateBackground();
    }
    private void GenerateObstacle()
    {
        GenerateElement(ChooseRandomPos(obstacleSpawnPoints), ChooseRandomObject(obstaclePrefabs),true);
    }       
    private void GenerateBackground()
    {
        GenerateElement(ChooseRandomPos(backgroundSpawnPoints), ChooseRandomObject(backgroundPrefabs),true);
    }   
    private void GenerateGate()
    {
        GenerateElement(ChooseRandomPos(gateSpawnPoints), ChooseRandomObject(gatePrefabs),true);
    }
    private void GenerateElement(Vector3 generatedPos,GameObject generatedObj,bool spawnWithChangingScale=false)
    {
        GameObject newObj=Instantiate(generatedObj, generatedPos, Quaternion.identity, this.transform);
        if (spawnWithChangingScale)
            NiceScaleChanging(newObj);
    }
    public static void NiceScaleChanging(GameObject newObj)
    {
        bool isSizeOk = false;
        newObj.transform.localScale = Vector3.one / 10;
        Observable.EveryFixedUpdate().TakeWhile(x => !isSizeOk).Subscribe(x =>
        {
            if (newObj.transform.localScale.x! <= 1f)
            {
                newObj.transform.localScale += Vector3.one / 50;
            }
            else
            {
                isSizeOk = true;
            }
        });
    }
    private GameObject ChooseRandomObject(GameObject[] gameObjects)
    {
        GameObject generatedObj = gameObjects[UnityEngine.Random.Range(0, gameObjects.Length - 1)];
        return generatedObj;
    }
    private Vector3 ChooseRandomPos(SpawnPoint[] spawnPoints)
    {
        Vector3 spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length - 1)].transform.position;
        return spawnPoint;
    }
}
