using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offsetCamera;
    public bool IsFollowing;

    private void Start()
    {
        IsFollowing = true;
    }

    void Update()
    {
        if (IsFollowing)
        {
            transform.position = player.transform.position + offsetCamera;
        }
    }
}