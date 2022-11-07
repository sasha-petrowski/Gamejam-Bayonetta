using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public List<WaveInfo> waves;
    [SerializeField]
    private GameObject prefabFlying;
    [SerializeField]
    private GameObject prefabGround;

    [SerializeField] private Transform cameraLockPosition;
    private List<EnemyInfos> currentEnnemies;
    
    private int currenteWave;
    private bool AlreadyTrigger = false;
    private bool HasStarted = false;

    private void Start()
    {
        currenteWave = 0;
        currentEnnemies = new List<EnemyInfos>();
    }

    private void Update()
    {
        if (HasStarted)
        {
            for (int i = 0; i < currentEnnemies.Count; i++)
            {
                if (currentEnnemies[i].life == 0)
                {
                    currentEnnemies.RemoveAt(i);
                }
            }
        }

        if (currentEnnemies.Count == 0)
        {
            currenteWave += 1;
            SpawnWave(currenteWave);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerTest>() && !AlreadyTrigger)
        {
            Debug.Log("JOUEUR and Lock Cam and Min/Max Pos ");
            AlreadyTrigger = true;
            SpawnWave(currenteWave);
        }
    }

    private void SpawnWave(int indexWave)
    {
        
    }
}

