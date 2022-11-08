using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public static int mult = 1;
    public static void AddScore(int add)
    {
        score += mult * add;
    }
}
