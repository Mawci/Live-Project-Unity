using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space_inv_game_manager : MonoBehaviour
{
    public GameObject[] alienWaveSet;
    public GameObject[] barriers;
    public GameObject backgroundPlanets;
    public GameObject backgroundStars;

    private GameObject currentSet;
    private Vector2 spawnPosition = new Vector2(0.48f, 5f);
    private static space_inv_game_manager instance;
    private bool firstWave = true;

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

    private void Start()
    {
        SpawnNewWave();
    }

    public static void SpawnNewWave()
    {
        instance.StartCoroutine(instance.SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        if(currentSet != null)
        {
            Destroy(currentSet);
        }

        yield return new WaitForSeconds(2);

        currentSet = Instantiate(alienWaveSet[UnityEngine.Random.Range(0, alienWaveSet.Length)], spawnPosition, Quaternion.identity);
        space_inv_HUD.UpdateWave();

        if(firstWave)
        {
            firstWave = false;
        }
        else
        {
            ResetBarriers();
            ChangeBackground();
        }
    }

    private void ResetBarriers()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].SetActive(true);
            barriers[i].GetComponent<space_inv_barrier>().ResetHealth();
        }
    }

    private void ChangeBackground()
    {
        backgroundPlanets.GetComponent<space_inv_background_controller>().ChangeBackgroundPlanets();
        backgroundStars.GetComponent<space_inv_background_controller>().ChangeBackgroundStars();
    }
}
