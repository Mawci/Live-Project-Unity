using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space_inv_barrier : MonoBehaviour
{
    public Sprite[] states;
    [SerializeField]private int health;
    private SpriteRenderer sRenderer;
    // Start is called before the first frame update
    void Start()
    {
        health = 4;
        sRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Determine if the collision is from an enemyProjectile, if it is, take damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            health--;

            if(health <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                sRenderer.sprite = states[health - 1];
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            health--;

            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                sRenderer.sprite = states[health - 1];
            }
        }
    }

    public void ResetHealth()
    {
        health = 4;
        sRenderer.sprite = states[health - 1];
    }
}
