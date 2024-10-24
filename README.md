# Live Project Unity

 Repository for my 2 week live project

## Scene Transitions
![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/loadScreen-ezgif.com-video-to-gif-converter.gif)


## Player Controls and Screen Bounds

![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/playerMovement.gif)

## Player Projectiles vs Enemy Projectiles


![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/ezgif.com-video-to-gif-converter.gif)

```c#

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
```

## Player Death

![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/playerDeath-video-to-gif-converter.gif)

## Faster Enemies

![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/fasterEnemies.gif)

## New Wave
![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/newWaveSpawn.gif)

called by the gameManager function on new wave load// talk about the reason for choosing setActive over destroy object
 ```c#

private void ResetBarriers()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].SetActive(true);
            barriers[i].GetComponent<space_inv_barrier>().ResetHealth();
        }
    }

```

the reset barrier function then calls the public function for each one
```c#
public void ResetHealth()
    {
        health = 4;
        sRenderer.sprite = states[health - 1];
    }
```


## Game Over with Persistent Data

![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/gameOver.gif)
