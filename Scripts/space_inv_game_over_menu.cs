using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class space_inv_game_over_menu : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;
    private int score;

    public TextMeshProUGUI highScoreValue;
    private int highScore;

    public TextMeshProUGUI waveValue;
    private int wave;

    private space_inv_score playerScore;
    // Start is called before the first frame update
    void Start()
    {
        playerScore = FindObjectOfType<space_inv_score>();
        scoreValue.text = playerScore.GetScore().ToString("000,000");
        waveValue.text = playerScore.GetWave().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
