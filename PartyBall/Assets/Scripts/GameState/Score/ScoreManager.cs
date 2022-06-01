using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int maxScore = 5;

    //Scores
    public int leftScore = 0;
    public int rightScore = 0;

    //On score events
    public UnityEvent leftScoreIncrease;
    public UnityEvent rightScoreIncrease;
    public UnityEvent resetScores;

    public void leftIncrease(){
        //Called when left side scores
        leftScore++;
        if(!checkWin())
            leftScoreIncrease.Invoke();
    }

    public void rightIncrease(){
        //Called when right side scores
        rightScore++;
        if(!checkWin())
            rightScoreIncrease.Invoke();
    }

    public void clearAll(){
        //Clear all scores
        leftScore = 0;
        rightScore = 0;
        resetScores.Invoke();
    }

    public bool checkWin(){
        //Check if anyone won; load next level if so
        if(leftScore >= maxScore || rightScore >= maxScore){
            //Set playerprefs
            PlayerPrefs.SetInt("TimeLeft", 1);
            PlayerPrefs.SetString("SideWon", GSM.scoreMgr.rightScore > GSM.scoreMgr.leftScore ? "Right" : "Left");
            PlayerPrefs.SetInt("LeftScore", GSM.scoreMgr.leftScore);
            PlayerPrefs.SetInt("RightScore", GSM.scoreMgr.rightScore);
            PlayerPrefs.Save();
            
            //Load end screen
            GetComponent<LoadLevel>().loadLast();
            return true;
        }
        return false;
    }
}
