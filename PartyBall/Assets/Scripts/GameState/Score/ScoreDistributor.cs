using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDistributor : MonoBehaviour
{
    //Bar of points
    public GameObject[] leftBar = new GameObject[5];
    public GameObject[] rightBar = new GameObject[5];

    //Indicator collors
    public Color offColor;
    public Color onColor;

    void Start(){
        //Listens for changes in score
        GSM.scoreMgr.leftScoreIncrease.AddListener(leftIncrease);
        GSM.scoreMgr.rightScoreIncrease.AddListener(rightIncrease);
        GSM.scoreMgr.resetScores.AddListener(clearAllPoints);
    }
    
    //Take in bar coming from "AddAsScoreBar"
    public void addToPoints(GameObject bar, int number, AddAsScoreBar.barSide barSide){
        //Set all to off by default
        bar.GetComponent<Image>().color = offColor;
        
        //Add to approprate array
        if(barSide == AddAsScoreBar.barSide.left){
            leftBar[number] = bar;
        } else {
            rightBar[number] = bar;
        }
    }

    private void leftIncrease(){
        //Turn on first available bar
        foreach(GameObject bar in leftBar){
            if(bar.GetComponent<Image>().color == offColor){
                bar.GetComponent<AddAsScoreBar>().setColor(onColor);
                break;
            }
        }
    }

    private void rightIncrease(){
        //Turn on first available bar
        foreach(GameObject bar in rightBar){
            if(bar.GetComponent<Image>().color == offColor){
                bar.GetComponent<AddAsScoreBar>().setColor(onColor);
                break;
            }
        }
    }

    private void clearAllPoints(){
        //Set all bars to clear
        foreach(GameObject bar in leftBar){
            bar.GetComponent<AddAsScoreBar>().setColor(offColor);
        }

        foreach(GameObject bar in rightBar){
            bar.GetComponent<AddAsScoreBar>().setColor(offColor);
        }
    }
}
