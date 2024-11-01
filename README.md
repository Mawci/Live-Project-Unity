# Live Project
## Introduction
During my time at the Tech Academy I joined a team of developers building a game in the Unity Engine for a 2 week sprint. This was an apprenticeship done for [Prosper IT Consulting](https://www.linkedin.com/company/prosper-it-consulting/) where I participated in Agile/Scrum practices by completing user stories (as well as sprint planning, daily stand-ups, sprint retrospective) to deliver a fully functional micro-game on time. I personally was tasked with creating a Space Invaders clone that would play in an arcade machine. I encountered numerous mechanics and features needing to be implemented that I knew nothing about. Not only did I successfully learn new material and deliver a completed product 3 days ahead of schedule, but I gained hands-on project management and team programming [skills](#other-skills-learned) that I know will be used and built upon in future projects.  

Below are descriptions of the stories I worked on, along with code snippets and navigation links. I also have full code files in this repo for the larger functionalities I implemented. If you would like to play the game, it has been published [here](https://play.unity.com/en/games/1e29f742-4101-4814-abab-023970facbcd/space-invaders-clone).


## User Stories
 * [Game Scenes](#game-scenes)
 * [Player Movement](#player-movement)
 * [Player Projectiles](#player-projectiles)
 * [Player Death](#player-death)
 * [Level Logic](#level-logic)
 * [New Level](#new-level)
 * [Game Over](#game-over)
 * [Skills](#other-skills-learned)
##

### Game Scenes
<br/>

&emsp; In this story I was responsible for creating a menu scene, game scene, and game-over scene. I worked with Unity’s UI elements to design a menu with buttons and custom font to more accurately represent the Space Invader theme. I referred to the Unity documentation and uploaded a new font to the project. From there, I was able to convert it to a new font asset that could be used as a Text Mesh Pro object.  
    

<p align=center>
<img src="https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/loadScreen-ezgif.com-video-to-gif-converter.gif" />
</p>
<br/>
<!--
![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/loadScreen-ezgif.com-video-to-gif-converter.gif)
-->

&emsp; There was already built-in functionality in the main game to handle scene transitions so instead of writing code for a new animation sequence, I created a local menu script that referenced the scene loader class. Then I referenced the project documentation to properly call and load the corresponding scenes on button click events. 

```c#
public class MenuScript : MonoBehaviour
{
    public SceneLoader loader;

    public void PlayGame()
    {
        loader.LoadSceneName("space_inv_game_scene");
    }

    public void GameOver()
    {
        loader.LoadSceneName("space_inv_game_over_scene");
    }

    public void SpaceInvadersMenu()
    {
        loader.LoadSceneName("space_inv_menu_scene");
    }
}
```
##

### Player Movement

![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/playerMovement.gif)

### Player Projectiles 


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

### Player Death

![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/playerDeath-video-to-gif-converter.gif)

### Level Logic
//talk about faster enemies
![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/fasterEnemies.gif)

### New Level
//new wave
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


### Game Over
//with persistent data

![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/gameOver.gif)


## Other Skills Learned

* Gained practical experience working as part of a team in a Scrum environment by attending daily stand-ups, sprint planning, and sprint retrospective meetings.
* Proven ability to integrate smoothly into ongoing development by adapting to custom naming conventions and workflows for checking out stories and creating pull requests in Azure DevOps.
* Experience with seeing problems before they arise and communicating them to the project manager immediately that resulted with no downtime to other developers or merge conflicts to the master branch.
* Communication with team members and assisting in problem solving. During a standup meeting I heard a fellow developer was having difficulty with the detection of 2D collisions and I was able to offer constructive solutions for troubleshooting.
* Ability to research and learn material that I am unfamiliar with. Several examples throughout this project where I needed to reference the Unity documentation to successfully implement features. (Animation events) (New Text Assets)
* Experience with encountering errors / bugs and stepping through them to discover the issue. Then creatively thinking through solutions to implement fixes.
