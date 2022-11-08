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
    private Vector3 cameraPreviousPos;
    [SerializeField] private GameObject prefabFlying;
    [SerializeField] private GameObject prefabGround;

    public List<EnemyInfos> currentEnnemies;

    private int currentWave;
    private bool AlreadyTrigger = false;
    private bool HasStarted = false;

    private void Start()
    {
        currentWave = 0;
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
                    currentEnnemies.RemoveAt(i);
                }
            }

            if (currentEnnemies.Count == 0)
            {
                currentWave += 1;
                if (currentWave >= waves.Count)
                {
                    Camera.main.transform.position = cameraPreviousPos;
                    Character.Instance.CameraBlock(false);
                    Camera.main.gameObject.GetComponent<FollowPlayer>().IsFollowing = true;
                }
                else
                {
                    SpawnWave(currentWave);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Character>() && !AlreadyTrigger)
        {
            StartCoroutine(FirstWave());
            Character.Instance.CameraBlock(true);
            Camera.main.gameObject.GetComponent<FollowPlayer>().IsFollowing = false;
            AlreadyTrigger = true;
        }
    }

    private IEnumerator FirstWave()
    {
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
        
    }
}