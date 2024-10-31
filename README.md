# Live Project
## Introduction
During my time at the Tech Academy I joined a team of developers building a game in the Unity Engine for a 2 week sprint. This was an apprenticeship done for [Prosper IT Consulting](https://www.linkedin.com/company/prosper-it-consulting/) where I participated in Agile/Scrum practices by completing user stories (as well as sprint planning, daily stand-ups, sprint retrospective) to deliver a fully functional micro-game on time. I personally was tasked with creating a Space Invaders clone that would play in an arcade machine. I encountered numerous mechanics and features needing to be implemented that I knew nothing about. Not only did I successfully learn new material and deliver a completed product 3 days ahead of schedule, but demonstrated necessary [comunication]() to the project manager that ensured there were no merge conflicts or down time during development. 

Below are descriptions of the stories I worked on, along with code snippets and navigation links. I also have full code files in this repo for the larger functionalities I implemented. Game is fully playable [here](https://play.unity.com/en/games/1e29f742-4101-4814-abab-023970facbcd/space-invaders-clone)


## User Stories
 * Scene Transitions
 * Player Movement
 * Player Projectiles
 * Player Death
 * Level Logic
 * New Level
 * Game Over

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
