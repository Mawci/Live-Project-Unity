using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class space_inv_HUD : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;
    private int score;

    public TextMeshProUGUI highScoreValue;
    private int highScore;

    public TextMeshProUGUI waveValue;
    private int wave;

    public Image[] lives;

    private Color32 activeColor = new Color(1,1,1,1);
    private Color32 inactiveColor = new Color(1, 1, 1,.15f);

    private static space_inv_HUD instance;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void UpdateLives(int amount)
    {
        foreach(Image i in instance.lives)
        {
            i.color = instance.inactiveColor;
        }

        for (int i = 0; i < amount; i++)
        {
            instance.lives[i].color = instance.activeColor;
        }
    }

    public static void UpdateScore(int amount)
    {
        instance.score += amount;
        instance.scoreValue.text = instance.score.ToString("000,000");
    }

    public static void UpdateWave()
    {
        instance.wave++;
        instance.waveValue.text = instance.wave.ToString();
    }

    public static void UpdateHighScore()
    {

    }

    public static int GetWave()
    {
        return instance.wave;
    }

    public static int GetScore()
    {
        return instance.score;
    }

    
}
