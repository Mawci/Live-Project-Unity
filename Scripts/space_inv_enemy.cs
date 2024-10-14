using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space_inv_enemy : MonoBehaviour
{
    public int score;

    public GameObject explosion;
    
    public void Kill()
    {
        space_inv_wave.alienWave.Remove(gameObject);
        space_inv_HUD.UpdateScore(score);
        Instantiate(explosion, transform.position, Quaternion.identity);

        if(space_inv_wave.alienWave.Count == 0)
        {
            space_inv_game_manager.SpawnNewWave();
        }
        Destroy(gameObject);
    }

    public void Die()
    {
        space_inv_wave.alienWave.Remove(gameObject);
        Destroy(gameObject);
    }

}
