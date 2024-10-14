using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space_inv_score : MonoBehaviour
{
    private int playerScore = 0;
    private int playerWave = 0;
    private int highScore = 0;

    private void Awake()
    {
        int numScoreSessions = FindObjectsOfType<space_inv_score>().Length;
        if(numScoreSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int num)
    {
        playerScore = num;
    }

    public void ResetScore()
    {
        playerScore = 0;
    }

    public void UpdateWave(int num)
    {
        playerWave = num;
    }

    public int GetScore()
    {
        return playerScore;
    }

    public int GetWave()
    {
        return playerWave;
    }
}
