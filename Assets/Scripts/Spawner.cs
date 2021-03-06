﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] fallingObstaclePrefabs;
    public float secondsBetweenSpawns = 1;
    float nextSpawnTime;

    public Vector2 spawnSizeMinMax;
    public float spawnAngleMax;

    Vector2 screenHalfSizeWorldUnits;

    // Start is called before the first frame update
    void Start()
    {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time>nextSpawnTime && !GameManager.Instance.gamePaused)
        {
            nextSpawnTime = Time.time + secondsBetweenSpawns;
            GameObject spawnObj = fallingObstaclePrefabs[Random.Range(0, fallingObstaclePrefabs.Length)];

            float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
            float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
            Vector2 spawnPos = new Vector2(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x),screenHalfSizeWorldUnits.y + spawnSize);
            GameObject newObstacle = (GameObject)Instantiate(spawnObj, spawnPos, Quaternion.Euler(Vector3.forward*spawnAngle));
            newObstacle.transform.localScale = Vector2.one * spawnSize;
        }

    }
}
