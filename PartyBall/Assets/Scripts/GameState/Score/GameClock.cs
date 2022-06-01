using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameClock : MonoBehaviour
{
    public int timeInSecondsStart = 60;

    //CurrentTimeLeft used to allow directly assigning the time while keeping within
    //legal clock times and automaticly update the timer
    private int currentTimeLeft;
    public int CurrentTimeLeft {
        get{
            return currentTimeLeft;
        }
        set{
            if(value >= 0){
                currentTimeLeft = value;
                updateTimer();
            }
        }
    } //Used to constrain to valid seconds and update on change

    public TextMeshProUGUI timerObj;
    
    public UnityEvent timerUp;

    void Start(){
        //Set to starting time
        CurrentTimeLeft = timeInSecondsStart;

        //Start a clock that will go down every second
        if(timerObj != null)
            StartCoroutine(startTimer());

        timerUp.AddListener(endGameTime);
    }

    private IEnumerator startTimer(){
        //Waits one second every second as long as there are seconds left
        while(currentTimeLeft > 0){
            yield return new WaitForSecondsRealtime(1);
            if(!PauseMenu.isPaused)
                CurrentTimeLeft -= 1;
        }

        //Time up
        timerUp.Invoke();
    }

    private void updateTimer(){
        string time = "";

        //Minutes
        time += (Mathf.Floor(CurrentTimeLeft/60)).ToString();
        
        time += ":";
        
        //Seconds with 2 points
        if(CurrentTimeLeft % 60 < 10)
            time += "0";
        time += (CurrentTimeLeft % 60).ToString();

        //Set clock
        if(timerObj != null)
            timerObj.text = time;
    }

    private void endGameTime(){
        //Game over
        //Game ended by time
        PlayerPrefs.SetInt("TimeLeft", 0);
        
        //Decide who won if anyone + Score keeping
        if(GSM.scoreMgr.rightScore == GSM.scoreMgr.leftScore){
            PlayerPrefs.SetString("SideWon", "Noone");
        } else {
            PlayerPrefs.SetString("SideWon", GSM.scoreMgr.rightScore > GSM.scoreMgr.leftScore ? "Right" : "Left");
        }

        PlayerPrefs.SetInt("LeftScore", GSM.scoreMgr.leftScore);
        PlayerPrefs.SetInt("RightScore", GSM.scoreMgr.rightScore);

        //Load end scene
        GetComponent<LoadLevel>().loadLast();
    }
}
