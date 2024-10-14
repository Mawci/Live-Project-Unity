using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space_inv_background_controller : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] bool scrollUp = true;

    public Sprite[] planets;
    public Sprite[] stars;
    private SpriteRenderer sRenderer;
    float singleTextureHeight;
    // Start is called before the first frame update
    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        SetupTexture();

        //Check if the direction of background should be moving down, if so the sign
        // must be switched
        if (!scrollUp)
        {
            moveSpeed = -moveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
        CheckReset();
    }

    private void SetupTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureHeight = sprite.texture.height / sprite.pixelsPerUnit;
    }

    private void Scroll()
    {
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(0f, delta, 0f);
    }

    //if the position of the background height hits the point of its height, reset the image
    private void CheckReset()
    {
        if ((Mathf.Abs(transform.position.y) - singleTextureHeight) > 0)
        {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }
    }

    public void ChangeBackgroundPlanets()
    {
        sRenderer.sprite = planets[UnityEngine.Random.Range(0, planets.Length)];
    }

    public void ChangeBackgroundStars()
    {
        sRenderer.sprite = stars[UnityEngine.Random.Range(0, stars.Length)];
    }
}
