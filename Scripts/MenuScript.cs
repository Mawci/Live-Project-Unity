using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public SceneLoader loader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        loader.LoadMainMenu();
    }

    public void PlayGame()
    {
        loader.LoadSceneName("space_inv_game_scene");
    }

    public void QuitGame()
    {
        Application.Quit();
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
