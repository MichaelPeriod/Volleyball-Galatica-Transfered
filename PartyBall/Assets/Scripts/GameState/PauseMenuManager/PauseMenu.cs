using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //Variable to control non-delta time and physics based game objects
    public static bool isPaused = false; //Used in GameClock

    //Dependancies
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] private LoadLevel levelLoader = null; //Used for loadByNumber function

    void Start(){
        //Create a level loader script if not already on
        if(levelLoader == null) levelLoader = gameObject.AddComponent(typeof(LoadLevel)) as LoadLevel;
    }

    void Update(){
        //On excape key pressed
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused)
                resume();
            else
                pause();
        }
    }

    public void resume(){
        //Resume the time and hide the menu
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void pause(){
        //Pause time and show the menu
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenuUI.SetActive(true);
    }

    public void loadMainMenu(){
        //Set game to default state before loading main menu
        resume();
        levelLoader.loadByNumber(0);
    }
}
