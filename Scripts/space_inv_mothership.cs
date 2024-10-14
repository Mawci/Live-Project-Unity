using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space_inv_mothership : MonoBehaviour
{
    public int score;

    private float maxLeft = -11f;
    private float speed = 4f;
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if(transform.position.x <= maxLeft)
        {
            Destroy(gameObject);
        }
    }

    public void Kill()
    {
        space_inv_HUD.UpdateScore(score);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
