using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class space_inv_wave : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject motherShipPrefab;

    //Distance Enemies move per increment
    private Vector3 horizontalDistance = new Vector3(0.1f, 0, 0);
    private Vector3 verticalDistance = new Vector3(0, 0.25f, 0);
    private Vector3 motherShipSpawn = new Vector3(9.5f, 4.72f, 0);

    //bounds of the game view
    private float maxLeft = -8.46f;
    private float maxRight = 8.38f;

    private float startY = -1f;
    //setting a max speed the enemies are allowed to move so that the speed
    //never becomes too difficult
    private float maxMoveSpeed = 0.02f;

    private float moveTimer = 0.01f;
    private float moveTime = .005f;

    private float shootTimer = 3f;
    private float shootTime = 3f;

    private float motherShipTimer = 60f;
    private float motherShipMinTime = 15f;
    private float motherShipMaxTime = 60f;

    public static List<GameObject> alienWave = new List<GameObject>();
    private bool movingRight;
    private bool entering = true;
    // Start is called before the first frame update
    void Start()
    {
        alienWave.Clear();
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            alienWave.Add(enemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(entering)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 10);

            if(transform.position.y <= startY)
            {
                entering = false;
            }
        }
        else
        {
            if (moveTimer <= 0)
            {
                MoveEnemies();
            }

            if (shootTimer <= 0)
            {
                Shoot();
            }

            if (motherShipTimer <= 0)
            {
                SpawnMotherShip();
            }

            motherShipTimer -= Time.deltaTime;
            shootTimer -= Time.deltaTime;
            moveTimer -= Time.deltaTime;
        } 
    }

    private void Shoot()
    {
        int rand = UnityEngine.Random.Range(0, alienWave.Count);
        Vector2 pos = alienWave[rand].transform.position;

        Instantiate(bulletPrefab, pos, Quaternion.identity);
        shootTimer = shootTime;
    }

    private void MoveEnemies()
    {
        if(alienWave.Count > 0)
        {
            //max is the varaible to check if the left or right bounds was touched
            int max = 0;
            for (int i = 0; i < alienWave.Count; i++)
            {
                
                if(movingRight)
                {
                    alienWave[i].transform.position += horizontalDistance;
                }
                else
                {
                    alienWave[i].transform.position -= horizontalDistance;
                }

                if(alienWave[i].transform.position.x > maxRight || alienWave[i].transform.position.x < maxLeft)
                {
                    max++;
                }
            }

            if (max > 0)
            {
                for (int i = 0; i < alienWave.Count; i++)
                {
                    alienWave[i].transform.position -= verticalDistance;
                }

                movingRight = !movingRight;
            }

            moveTimer = GetMoveSpeed();
        }
    }

    private void SpawnMotherShip()
    {
        Instantiate(motherShipPrefab, motherShipSpawn, Quaternion.identity);
        motherShipTimer = UnityEngine.Random.Range(motherShipMinTime, motherShipMaxTime);
    }

    //Multiply the enemies remaining by a small variable so the speed gets faster as
    // there are less enemies remaining
    private float GetMoveSpeed()
    {
        float time = alienWave.Count * moveTime;

        if(time < maxMoveSpeed)
        {
            return maxMoveSpeed;
        }
        else
        {
            return time;
        } 
    }
}
