using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public static PlayerTest Instance;

    private void Awake()
    {
        Instance = this;
    }

}
