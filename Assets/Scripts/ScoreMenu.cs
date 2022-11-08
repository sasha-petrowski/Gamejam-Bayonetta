using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreMenu : MonoBehaviour
{
    public GameObject scoreMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    private void Update()
    {
        scoreText.text = ScoreManager.score.ToString();
        timeText.text = $"{Mathf.FloorToInt(Time.time)} S";
    }
}
