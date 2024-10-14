using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space_inv_enemy_bullet : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float delta = speed * Time.deltaTime;
        transform.Translate(0f, -delta, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
