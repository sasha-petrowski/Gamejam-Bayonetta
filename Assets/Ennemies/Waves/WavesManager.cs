using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public List<WaveInfo> waves;
    public List<Transform> flyingSpawners = new List<Transform>();
    public List<Transform> groundSpawners = new List<Transform>();

    [SerializeField] private Camera cam;
    [SerializeField] private GameObject prefabFlying;
    [SerializeField] private GameObject prefabGround;

    [SerializeField] private Transform cameraLockPosition;
    public List<EnemyInfos> currentEnnemies;

    private int currentWave;
    private bool AlreadyTrigger = false;
    private bool HasStarted = false;
    private bool IsLocked = false;
    public int compt;

    private void Start()
    {
        currentWave = 0;
        compt = 0;
        currentEnnemies = new List<EnemyInfos>();
    }

    private void Update()
    {
        if (HasStarted)
        {
            for (int i = 0; i < currentEnnemies.Count; i++)
            {
                if (currentEnnemies[i].life <= 0)
                {
                    compt -= 1;
                    currentEnnemies.RemoveAt(i);
                }
            }
            if (currentEnnemies.Count == 0)
            {
                currentWave += 1;
                if (currentWave >= waves.Count)
                {
                    IsLocked = false;
                }
                else
                {
                    SpawnWave(currentWave);
                }
            }
        }

        if (IsLocked)
        {
            cam.transform.position = cameraLockPosition.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerPosition>() && !AlreadyTrigger)
        {
            StartCoroutine(FirstWave());
            AlreadyTrigger = true;
        }
    }

    private IEnumerator FirstWave()
    {
        cam.transform.DOMove(cameraLockPosition.position, 0.5f);
        yield return new WaitForSeconds(2);
        SpawnWave(currentWave);
        HasStarted = true;
    }


    private void SpawnWave(int indexWave)
    {
        for (int i = 0; i < waves[indexWave].numberOfFlyingEnnemies; i++)
        {
            Spawn(prefabFlying, i, flyingSpawners[(i) % flyingSpawners.Count].position);
        }

        for (int i = 0; i < waves[indexWave].numberOfGroundEnnemies; i++)
        {
            Spawn(prefabGround, i, groundSpawners[(i) % groundSpawners.Count].position);
        }
    }

    private void Spawn(GameObject go, int i, Vector3 pos)
    {
        EnemyInfos enemyInfos = Instantiate(go, pos, Quaternion.identity).GetComponent<EnemyInfos>();
        currentEnnemies.Add(enemyInfos);
        compt += 1;
    }
}