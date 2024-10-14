using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space_inv_player_controller : MonoBehaviour
{
    public space_inv_stats playerStats;

    public GameObject bulletPrefab;

    private Vector2 deathPosition = new Vector2(0, -30f);
    private Vector2 startPosition = new Vector2(-.05f, -4.23f);
    public MenuScript menu;

    private float respawnTime = 2f;
    private float maxLeft = -8.46f;
    private float maxRight = 8.38f;
    private bool isShooting;

    private space_inv_score playerScore;

    // Start is called before the first frame update
    void Start()
    {
        playerStats.currentHealth = playerStats.maxHealth;
        playerStats.currentLives = playerStats.maxLives;
        transform.position = startPosition;

        space_inv_HUD.UpdateLives(playerStats.currentLives);
        playerScore = FindObjectOfType<space_inv_score>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= maxLeft && transform.position.x <= maxRight)
        {
            float translation = Input.GetAxis("Horizontal") * playerStats.shipSpeed;
            translation *= Time.deltaTime;
            transform.Translate(translation, 0, 0);
        }

        if(transform.position.x < maxLeft)
        {
            transform.position = new Vector2(maxLeft, transform.position.y);
        }

        if (transform.position.x > maxRight)
        {
            transform.position = new Vector2(maxRight, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    private void TakeDamage()
    {
        //Having a health variable allows for the player to be hit more than once before 
        //dying. Currently leaving the health at 1 so that death is always 1 shot. 
        playerStats.currentHealth--;

        if(playerStats.currentHealth == 0)
        {
            playerStats.currentLives--;
            space_inv_HUD.UpdateLives(playerStats.currentLives);

            if(playerStats.currentLives == 0)
            {
                Debug.Log("Game Over");
                playerScore.UpdateScore(space_inv_HUD.GetScore());
                playerScore.UpdateWave(space_inv_HUD.GetWave());
                menu.GameOver();
            }
            else
            {
                Debug.Log("Respawing..");
                StartCoroutine(Respawn());
            }
        }
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(playerStats.fireRate);
        isShooting = false;
    }

    private IEnumerator Respawn()
    {
        transform.position = deathPosition;
        yield return new WaitForSeconds(respawnTime);
        playerStats.currentHealth = playerStats.maxHealth;
        transform.position = startPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player Hit!");
            TakeDamage();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player Hit!");
            TakeDamage();
            collision.gameObject.GetComponent<space_inv_enemy>().Die();
        }
    }
}
