# Live Project
## Introduction
&emsp;During my time at the Tech Academy I joined a team of developers building a game in the Unity Engine for a 2 week sprint. This was an apprenticeship done for [Prosper IT Consulting](https://www.linkedin.com/company/prosper-it-consulting/) where I participated in Agile/Scrum practices by completing user stories (as well as sprint planning, daily stand-ups, sprint retrospective) to deliver a fully functional micro-game on time. I personally was tasked with creating a Space Invaders clone that would play in an arcade machine. I encountered numerous mechanics and features needing to be implemented that I knew nothing about. Not only did I successfully learn new material and deliver a completed product 3 days ahead of schedule, but I gained hands-on project management and team programming [skills](#other-skills-learned) that I know will be used and built upon in future projects.  

&emsp;Below are descriptions of the stories I worked on, along with code snippets, gifs, and navigation links. I also have full code files in this repo for the larger functionalities I implemented. If you would like to play the game, it has been published [here](https://play.unity.com/en/games/1e29f742-4101-4814-abab-023970facbcd/space-invaders-clone).

## User Stories
 * [Game Scenes](#game-scenes)
 * [Player Movement](#player-movement)
 * [Player Abilites](#player-abilities)
 * [Environment](#environment)
 * [Animations](#animations)
 * [Enemies](#enemies)
 * [New Level](#new-level)
 * [Game Over](#game-over)
 * [Skills](#other-skills-learned)
   <!--* [Player Death](#player-death)-->
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
*Jump To: [Page Top](#introduction), [Player Abilites](#player-abilities), [Environment](#environment), [Animations](#animations), [Enemies](#enemies), [New Level](#new-level), [Game Over](#game-over), [Skills](#other-skills-learned)*
##

### Player Movement

&emsp;This story was seemingly simple as the movement in Space Invaders is not too complex, however I still ran into a bug that I needed to carefully step through. Initially, I used transform.Translate() to move the player using its horizontal vector multiplied by a constant speed. This worked, but I needed a way to ensure the player stayed within the viewable bounds of the screen. To solve this, I created two variables that would hold the maximum position on the right and left sides of the screen before going out of view. Then I did a simple check to see if the player was within those two values. If they were, then allow input to move the player.
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
&emsp;With the addition of the two lines of code above, the player now gets instantly micro-adjusted to be within the screen bounds. As you can see below, seemingly resulting in an "invisible barrier" feel.

<p align=center>
<img src="https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/playerMovement.gif" />
</p>
<!--
![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/playerMovement.gif)
-->

*Jump To: [Page Top](#introduction), [Game Scenes](#game-scenes), [Environment](#environment), [Animations](#animations), [Enemies](#enemies), [New Level](#new-level), [Game Over](#game-over), [Skills](#other-skills-learned)*

##

### Player Abilities


&emsp;For this story I was tasked with completing the abilities for the player. Seemingly simple as the player only has the “shoot” ability, I learned just how tedious it needed to be to fully encompass an original Space Invader clone. Initially, I worked with the player controller script to register input appropriately. Then made the player projectile by combining Unity’s sprite renderer component, 2DBoxCollider, 2DRigidBody, with a custom movement script that gives constant speed in the y axis.
<br/>
<br/>
<p align=center>
    <img src="https://github.com/Mawci/Live-Project-Unity/blob/main/images/bulletProjectile.png" />
</p>


#### Bullet Spamming Bug
&emsp;The above implementation worked. However, it introduced an unintended feature. The player could hit the spacebar as fast as they wanted to shoot rapidly. Part of the difficulty in Space Invaders is the rate of fire and the necessity to plan your shots as the enemies dwindle. To prevent the player from spamming, I implemented a coroutine that would call the shoot function. The time passed into WaitForSeconds() would simulate the fire rate with a simple boolean flag to check if the coroutine is active or not. In the code below, it is the isShooting variable

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
&emsp;If isShooting is true, don’t allow the player to shoot another projectile. Only when isShooting is false can the player shoot a projectile. Now you can see below, the steady rate of fire, even though I was pressing the space bar as fast as I could. 
<p align=center>
    <img src="https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/playerShooting.gif" />
</p>

#### Communicating Early
&emsp;When I created the prefab for this projectile and added the physics layer / object tag, I saw all pre-existing physics layers in the project. After reading the project documentation, I realized the necessity for additional physics layers to be added so that the Space Invaders clone could behave in the same manner as its original. To be clear, this was before adding any enemies or shields into the game. I knew ahead of time that I would be detecting/ignoring collisions differently than what the current settings of the project allotted for. Specifically, I needed the player projectile to collide with the enemy projectile and the enemy while ignoring the player and the shield (allowing the player to strategically be positioned behind the shield and shoot through it). Simultaneously, I knew that the enemy projectile would need to collide with the shield, the player projectile, and the player while also ignoring the other enemies.

&emsp;At this point in development, I reached out to the project manager communicating the need for these additions to the project. Since I did this as soon as I noticed the need, proper additions were made to the project’s collision matrix resulting in zero slow downs to other developers or merge conflicts in pull requests. In fact, when I started working on the Environment / Enemy stories a couple days later. I was able to start and complete my deliverables without any delay. If I had said nothing and just waited until I encountered that problem within the story, I would have been at a roadblock, unable to continue development for at least 2 days


&emsp;Below is the newly edited collision matrix that I was permitted to make additions to. Notice how the "Shield", "Enemy Bullet", and "Player Bullet" have all been newly added on the left column with logic that is unique to any other previous layers.

<p align=center>
    <img src="https://github.com/Mawci/Live-Project-Unity/blob/main/images/collisionMatrix.png" />
</p>

*Jump To: [Page Top](#introduction), [Game Scenes](#game-scenes), [Player Movement](#player-movement), [Animations](#animations), [Enemies](#enemies), [New Level](#new-level), [Game Over](#game-over), [Skills](#other-skills-learned)*
##

### Environment

<!--### <p align="center">1.)Background</p>-->
&emsp;In this story I began by implementing a background that scrolls to make it look like the player is moving through space. This is considered a parallax background where stacked images seem to “scroll endlessly” in the background. Through researching how this effect is achieved, I was able to write a script that takes a sprite image, moves it slowly, and resets the position every time it reaches a certain distance. By cleverly tracking its position and resetting it when it is divisible by its tiled height, the player doesn’t notice when the background image has reset.


First I cached the reference to the sprite and calculated the amount of pixels in one unit of the sprite's height as seen below.

```c#
private void SetupTexture()
{
    Sprite sprite = GetComponent<SpriteRenderer>().sprite;
    singleTextureHeight = sprite.texture.height / sprite.pixelsPerUnit;
}
```

Then Scroll() is called in the Update method to move the image, followed by a check to see if it is ready to be reset.

```c#
private void Scroll()
{
    float delta = moveSpeed * Time.deltaTime;
    transform.position += new Vector3(0f, delta, 0f);
}

private void CheckReset()
{
    if ((Mathf.Abs(transform.position.y) - singleTextureHeight) > 0)
    {
        transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
    }
}
```

<p align=center>
    <img src="https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/movingBackground.gif" /></p>
    
###### <p align="center"> The reset you see above is actually the video recording reset!<br/> The reset of the background happens here without you noticing.<br/> Try [playing the game](https://play.unity.com/en/games/1e29f742-4101-4814-abab-023970facbcd/space-invaders-clone) to see how seamless it is.</p><br/>

<!--### <p align="center">2.)Shields</p>-->
&emsp;The second part of this story was creating the shields that the player can take cover behind and effectively shoot through. However, when the shield takes damage from an enemy projectile, it needs to show a damaged state. To achieve this effect I created a script that holds an array of sprites. Each element in the array represents a different health state. Then when the correct collision happens, set the sprite renderer to the next element in the array. 

&emsp;In the code below, the shield object checks if the collision is from an enemy projectile. If it is, then destroy the projectile and decrement the health of the shield. After decrementing, check to see if the shield needs to be destroyed or changed to a different sprite. 

```c#
private void OnCollisionEnter2D(Collision2D collision)
{
    if(collision.gameObject.CompareTag("EnemyBullet"))
    {
        Destroy(collision.gameObject);
        health--;

            if(health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                sRenderer.sprite = states[health - 1];
            }
    }
}
```

As you can see in the gif below from the completed game, the player is able to shoot through the shield and the shield changes its sprite only when hit by an enemy projectile.
<p align=center>
    <img src="https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/ezgif.com-video-to-gif-converter.gif" /></p>

#### Shield Error
&emsp;Unfortunately, the way that I initially implemented this shield would cause a game crash later on in development. I needed to come back to this script when I encountered a null reference exception from attempting to reset the health of the shield for a new wave. Since I called Destroy(gameObject) on the shield whenever the health reached zero, I could no longer access it inside of the game manager script when I needed to reset its health because it didn’t exist.

&emsp;To solve this problem, I changed one line of code from above, to what you now see below. The .SetActive() method changes the active state of the game object to false instead of destroying it. Therefore, the shield still exists when its health reaches zero and is simply invisible to the player. This fixed the issue and the game no longer crashed when attempting to load a new wave.

```c#
if(health <= 0)
{
    gameObject.SetActive(false);
}

```


*Jump To: [Page Top](#introduction), [Game Scenes](#game-scenes), [Player Movement](#player-movement), [Player Abilites](#player-abilities), [Enemies](#enemies), [New Level](#new-level), [Game Over](#game-over), [Skills](#other-skills-learned)*

##

### Animations

&emsp;In this story I created and became quite efficient with the workflow for 2D animations. From importing, splicing, and editing sprites, to creating animation controllers and sequences. I built and tweaked the animations for the enemies to better match the speed of the original Space Invaders game. 

#### Explosion Particle Bug 
&emsp;One issue I ran into when creating the animations was the explosion particle. Since I was attempting to instantiate the explosion particle as a game object at the location an enemy died, the last frame of the explosion would stay on the screen permanently. I needed to destroy the particle, but only after the animation had fully played once. I researched and referenced Unity’s documentation for solutions and found animation events. To solve this issue I implemented an animation event *(seen below)* on the last frame to call a function that would destroy itself.
<p align=center>
    <img src="https://github.com/Mawci/Live-Project-Unity/blob/main/images/animationEvent.png" /></p>
    
&emsp;This ensured all responsibilities for the explosion particle’s existence were self contained by accessing its own kill method *(seen below)* to remove itself from the game after playing its animation. This was a quick and simple fix for the implementation I chose to do.  

```c#
public class space_inv_explosion : MonoBehaviour
{
     public void Kill()
    {
        Destroy(gameObject);
    }
}
```


*Jump To: [Page Top](#introduction), [Game Scenes](#game-scenes), [Player Movement](#player-movement), [Player Abilites](#player-abilities), [Environment](#environment), [New Level](#new-level), [Game Over](#game-over), [Skills](#other-skills-learned)*

##

### Enemies

&emsp;This was arguably the most difficult challenge I faced as I needed to move the entire wave of enemies at a set interval while still keeping track of the total number of enemies remaining on screen that would proportionally increase their speed. To tackle this problem, I made 2 scripts. One to hold a list of all the enemies so that they could be moved as a whole, and the other would reside on the enemy to be responsible for destroying and removing itself from the list. 

In the wave script, I start by adding each object in the scene with the tag of “enemy” to the list.

```c#
void Start()
{
    foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            alienWave.Add(enemy);
        }
}
```
Then, by keeping track and updating a float variable called "moveTimer" within the Update() function, I call a function named MoveEnemies() to loop through and move each enemy a set distance. By playtesting and tweaking this timer, you get a feeling of the enemies moving in "steps." 

```c#
void Update()
{
    if (moveTimer <= 0)
    {
         MoveEnemies();
    }

   moveTimer -= Time.deltaTime;
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
```
&emsp;Above you can see the bool flag named "movingRight" to tell what direction the enemies should move. I combined this with a check after each move to see if the enemy wave hit the bounds of the screen called "max." If max was a value greater than 0, meaning the bounds of the screen were hit, then the enemies were moved down, changed directions, and max reset. After all movemment was complete, I called the GetMoveSpeed() function *(defined below)* to change the speed of the enemies based on the amount remaining. 

```c#
private float GetMoveSpeed()
{
    float time = alienWave.Count * moveTime;
    return time;
}
```
This gives the intended result of what you see below. When the number of enemies decrease, so does the time between each move interval.

<p align=center>
    <img src="https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/fasterEnemies.gif" /></p>

#### Enemy Wave Error

&emsp;Through thorough playtesting later in development, I discovered a crash that would occur with the wave script when the player attempted to play again. I came back to this script to debug it and found a situation that could occur where enemies are added to the list that's not empty. This scenario specifically occurs when a player dies in a previous game and launches another. All the enemies from the previous game stay in the list and the new game enemies are added to it. Then when the wave script attempts to move the enemies, it crashes because the enemies from the previous game no longer exist. By stepping through the problem to see why the error was happening, I was able to add a simple line of code *(seen below)* to clear the list of enemies to always ensure an empty list on a new game. 

```c#
void Start()
    {
        alienWave.Clear();
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            alienWave.Add(enemy);
        }
    }
```

*Jump To: [Page Top](#introduction), [Game Scenes](#game-scenes), [Player Movement](#player-movement), [Player Abilites](#player-abilities), [Environment](#environment), [Animations](#animations), [Game Over](#game-over), [Skills](#other-skills-learned)*

##
<!--
### Player Death

![](https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/playerDeath-video-to-gif-converter.gif)
*Jump to: [top](#live-project)*

##
-->
### New Level

&emsp;In this story I was responsible for adding additional levels to the game. To accomplish this, I added a game manager script with a method to load a new wave of enemies. Going into this, I knew I wanted other scripts in the game to have access to the functions in the game manager such as an ability to spawn a new wave, update the UI, or reset the health of the barriers. Therefore I made use of the singleton pattern.


```c#
private static space_inv_game_manager instance;
private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
```

I then made a coroutine that will wait 2 seconds before spawning in a new wave that randomly instantiates an enemy wave prefab which allows for different arrangements of enemies as the player progresses through the game.

```c#
public static void SpawnNewWave()
{
   instance.StartCoroutine(instance.SpawnWave());
}

private IEnumerator SpawnWave()
{
   if(currentSet != null)
   {
      Destroy(currentSet);
   }

   yield return new WaitForSeconds(2);

   currentSet = Instantiate(alienWaveSet[UnityEngine.Random.Range(0, alienWaveSet.Length)], spawnPosition, Quaternion.identity);
   space_inv_HUD.UpdateWave();

   if(firstWave)
   {
      firstWave = false;
   }
   else
   {
      ResetBarriers();
      ChangeBackground();
   }
}
```
The boolean flag "firstWave" above checks to see if the level needs to reload the barriers or change the background. After the initial wave, every call to the method will reset all the shields health and change the background. Those methods are defined below. 

```c#
private void ResetBarriers()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].SetActive(true);
            barriers[i].GetComponent<space_inv_barrier>().ResetHealth();
        }
    }

    private void ChangeBackground()
    {
        backgroundPlanets.GetComponent<space_inv_background_controller>().ChangeBackgroundPlanets();
        backgroundStars.GetComponent<space_inv_background_controller>().ChangeBackgroundStars();
    }
```
&emsp;Coincidently, this is where I ran into the error with the [shield](#shield-error) and had to spend time debugging. Now properly fixed, you can see how the game manager spawns in a new wave when the last enemy is defeated, the shields are set back to full health, the background is changed, and the wave number in the upper right corner is updated. 

<p align=center>
    <img src="https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/newWaveSpawn.gif" /></p>


*Jump To: [Page Top](#introduction), [Game Scenes](#game-scenes), [Player Movement](#player-movement), [Player Abilites](#player-abilities), [Environment](#environment), [Animations](#animations), [Enemies](#enemies), [Skills](#other-skills-learned)*
##

### Game Over

&emsp;In this story I needed to trigger a game over whenever the player would lose their last life. To do this, I simply added a check in the player script to see if the player lives were equal to zero. If they were equal to zero, load the game over scene.
```c#
if(playerStats.currentLives == 0)
{
   playerScore.UpdateScore(space_inv_HUD.GetScore());
   playerScore.UpdateWave(space_inv_HUD.GetWave());
   menu.GameOver();
}
```
&emsp;The problem with this, however, is that the score and wave number would not persist to the game over screen. The player wouldn’t see how many points they accumulated or waves survived because the player score script creates a new instance on scene load. To properly achieve score that persists across scenes, I referenced Unity’s documentation to effectively implement the “DontDestoryOnLoad()” method. You can see how I added it to the player score script below. 

```c#
private void Awake()
{
   int numScoreSessions = FindObjectsOfType<space_inv_score>().Length;
   if(numScoreSessions > 1)
   {
      Destroy(gameObject);
   }
   else
   {
   DontDestroyOnLoad(gameObject);
   }
}
```
Now with this added, the score will persist onto the game over scene to show how many waves were survived with their total points.

<p align=center>
    <img src="https://github.com/Mawci/Live-Project-Unity/blob/main/Gifs/gameOver.gif" /></p>

## Other Skills Learned

* Gained practical experience collaborating with a team of developers in a Scrum environment, participating in sprint planning, retrospectives, and daily stand-ups to enhance project efficiency.
* Demonstrated adaptability by seamlessly integrating into ongoing development, quickly mastering custom naming conventions and workflows for Azure DevOps, and utilizing Git for version control.
* Proactively identified potential issues before they escalated, [communicating](#communicating-early) effectively with the project manager to prevent downtime and ensure smooth development processes.
* Actively communicated with team members, providing support in problem-solving during stand-up meetings. For example, I assisted a fellow developer in troubleshooting 2D collision detection, resulting in a faster resolution.
* Ability to research and learn material that I am unfamiliar with. Several examples throughout this project where I needed to reference the Unity documentation to successfully implement features.
  * [Animation events](#explosion-particle-bug)
  * [New Font Assets](#game-scenes)
  * [Parallax Background](#environment)
  * [Persistent Data](#game-over)
* Gained hands-on experience in debugging by systematically identifying and resolving errors, fostering a deeper understanding of the development process and enhancing my problem-solving abilities.
  * [Input Bug](#movement-bug)
  * [Bullet Spamming](#bullet-spamming-bug)
  * [Shield Error](#shield-error)
  * [Explosion Particle](#explosion-particle-bug)
  * [Wave Error](#enemy-wave-error)

*Jump To: [Page Top](#introduction), [Game Scenes](#game-scenes), [Player Movement](#player-movement), [Player Abilites](#player-abilities), [Environment](#environment), [Animations](#animations), [Enemies](#enemies), [New Level](#new-level)*
