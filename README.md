# Live Project
## Introduction
During my time at the Tech Academy I joined a team of developers building a game in the Unity Engine for a 2 week sprint. This was an apprenticeship done for [Prosper IT Consulting](https://www.linkedin.com/company/prosper-it-consulting/) where I participated in Agile/Scrum practices by completing user stories (as well as sprint planning, daily stand-ups, sprint retrospective) to deliver a fully functional micro-game on time. I personally was tasked with creating a Space Invaders clone that would play in an arcade machine. I encountered numerous mechanics and features needing to be implemented that I knew nothing about. Not only did I successfully learn new material and deliver a completed product 3 days ahead of schedule, but I gained hands-on project management and team programming [skills](#other-skills-learned) that I know will be used and built upon in future projects.  

Below are descriptions of the stories I worked on, along with code snippets and navigation links. I also have full code files in this repo for the larger functionalities I implemented. If you would like to play the game, it has been published [here](https://play.unity.com/en/games/1e29f742-4101-4814-abab-023970facbcd/space-invaders-clone).


## User Stories
 * [Game Scenes](#game-scenes)
 * [Player Movement](#player-movement)
 * [Player Abilites](#player-abilities)
 * [Environment]()
 * [Enemies](#enemies)
 * [Animations]()
 * [Player Death](#player-death)
 * [New Level](#new-level)
 * [Game Over](#game-over)
 * [Skills](#other-skills-learned)
##

### Game Scenes


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

&emsp;This story was seemingly simple as the movement in Space Invaders is not too complex, however I still ran into a bug that I needed to carefully step through. Initially, I used transform.Translate() to move the player using its horizontal vector multipied by a constant speed. This worked, but I needed a way to ensure the player stayed within the viewable bounds of the screen. To solve this, I created two variables that would hold the maximum position on the right and left sides of the screen before going out of view. Then I did a simple check to see if the player was within those two values. If they were, then allow input to move the player.
<br/> <br/>

```c#
if(transform.position.x >= maxLeft && transform.position.x <= maxRight)
{
     float translation = Input.GetAxis("Horizontal") * playerStats.shipSpeed;
     translation *= Time.deltaTime;
     transform.Translate(translation, 0, 0);
}
```
#### Movement Bug
&emsp;Through playtesting, I discovered that this wouldn’t always work. Since I was using transform.Translate() to move, the player would sometimes move faster than the check to see if it was within the bounds. Therefore, it would go past the boundary to then be perpetually stuck and no longer able to register input. To solve this bug I added two additional checks after the player movement to see if the player was outside the bounds. If they were outside the bounds, (which would mean they can never move again) then reset the player position back to the maximum right/left position. This way, if a player managed to go past the edge, the next line of code would quickly reposition them so that the next Update function call (checking if the player position is valid) would return true, allowing the player to move again.

```c#
if(transform.position.x < maxLeft)
{
    transform.position = new Vector2(maxLeft, transform.position.y);
}

if (transform.position.x > maxRight)
{
    transform.position = new Vector2(maxRight, transform.position.y);
}
```
With the addition of the two lines of code above, the player now gets instantly micro-adjusted to be within the screen bounds. As you can see below, seemlessly resulting in an "invisible barrier" feel.

<p align=center>
<img src="https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/playerMovement.gif" />
</p>
<!--
![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/playerMovement.gif)
-->

##

### Player Abilities


&emsp;For this story I was tasked with completing the abilities for the player. Seemingly simple as the player only has the “shoot” ability, I learned just how tedious it needed to be to fully encompass an original Space Invader clone. Initially, I worked with the player controller script to register input appropriately. Then made the player projectile by combining Unity’s sprite renderer component, 2DBoxCollider, 2DRigidBody, with a custom movement script that gives constant speed in the y axis.
<br/>
<br/>
<p align=center>
    <img src="https://github.com/Mawci/Live-Project-Unity/blob/main/images/bulletProjectile.png" />
</p>


#### Bullet Spamming Bug
The above implementation worked. However, it introduced an unintended feature. The player could hit the spacebar as fast as they wanted to shoot rapidly. Part of the difficulty in Space Invaders is the rate of fire and the necessity to plan your shots as the enemies dwindle. To prevent the player from spamming, I implemented a coroutine that would call the shoot function. The time passed into WaitForSeconds() would simulate the fire rate with a simple boolean flag to check if the coroutine is active or not. In the code below, it is the isShooting variable

```c#
// In the Update Method 
if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
{
    StartCoroutine(Shoot());
}

// Shoot function called when space is pressed and isShooting is false
private IEnumerator Shoot()
{
    isShooting = true;
    Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    yield return new WaitForSeconds(playerStats.fireRate);
    isShooting = false;
}
```
If isShooting is true, don’t allow the player to shoot another projectile. Only when isShooting is false can the player shoot a projectile. 

#### Communicating Early
When I created the prefab for this projectile and added the physics layer / object tag, I saw all pre-existing physics layers in the project. After reading the project documentation, I realized the necessity for additional physics layers to be added so that the Space Invaders clone could behave in the same manner as its original. To be clear, this was before adding any enemies or shields into the game. I knew ahead of time that I would be detecting/ignoring collisions differently than what the current settings of the project allotted for. Specifically, I needed the player projectile to collide with the enemy projectile and the enemy while ignoring the player and the shield (allowing the player to strategically be positioned behind the shield and shoot through it). Simultaneously, I knew that the enemy projectile would need to collide with the shield, the player projectile, and the player while also ignoring the other enemies. At this point in development, I reached out to the project manager communicating the need for these additions to the project. Since I did this as soon as I noticed the need, proper additions were made to the project’s collision matrix resulting in zero slow downs to other developers or merge conflicts in pull requests. In fact, when I started working on the Environment / Enemy stories a couple days later. I was able to start and complete my deliverables without any delay. If I had said nothing and just waited until I encountered that problem within the story, I would have been at a roadblock, unable to continue development for at least 2 days

Jump to see how smoothly the gameObject collisions went in the [enemy story]()
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

### Enemies
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

* Gained practical experience working as part of a team in a Scrum environment by attending daily , sprint planning, sprint retrospective, and daily stand-up meetings.
* Proven ability to integrate smoothly into ongoing development by adapting to custom naming conventions and workflows for checking out stories and creating pull requests in Azure DevOps.
* Experience with seeing problems before they arise and communicating them to the project manager immediately that resulted with no downtime to other developers or merge conflicts to the master branch.
* Communication with team members and assisting in problem solving. During a standup meeting I heard a fellow developer was having difficulty with the detection of 2D collisions and I was able to offer constructive solutions for troubleshooting.
* Ability to research and learn material that I am unfamiliar with. Several examples throughout this project where I needed to reference the Unity documentation to successfully implement features. (Animation events) (New Text Assets)
* Experience with encountering errors / bugs and stepping through them to discover the issue. Then creatively thinking through solutions to implement fixes.
