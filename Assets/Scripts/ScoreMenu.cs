using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreMenu : MonoBehaviour
{
    public GameObject scoreMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    public void UpdateScoreMenu()
    {
        scoreMenu.SetActive(true);
        scoreText.text = ScoreManager.score.ToString();
        timeText.text = $"{Time.time / 60} min";
    }
}
