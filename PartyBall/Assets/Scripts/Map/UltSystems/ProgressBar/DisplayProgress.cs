using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayProgress : MonoBehaviour
{
    //What the bar goes up to
    private const float maxBar = 100f;

    //Left bar info
    [SerializeField] private GameObject leftBar;
    [SerializeField][Range(0f,maxBar)] private float leftPercentage = 0;

    //Right bar info
    [SerializeField] private GameObject rightBar;
    [SerializeField][Range(0f,maxBar)] private float rightPercentage = 0;
    
    //Side the button resides in
    public enum buttonSide{
        red,
        blue
    };

    void Start(){
        //Set bar to cleared by default
        clearBar();
    }

    private void updateBar(){
        //Check if bars exist
        //Calculate precentage based on full bar
        //Scale is equal to 1 when 100%
        //Offset position to acount for left or right sidedness
        if(leftBar != null){
            float percentage = leftPercentage / maxBar;
            leftBar.transform.localPosition = new Vector3(percentage / 2f -.5f, 0f, 0f);
            leftBar.transform.localScale = new Vector3(percentage, 1f, 1f);
        }

        if(rightBar != null){
            float percentage = rightPercentage / maxBar;
            rightBar.transform.localPosition = new Vector3(.5f - percentage / 2f, 0f, 0f);
            rightBar.transform.localScale = new Vector3(percentage, 1f, 1f);
        }
    }

    public bool addPoints(buttonSide sideToAdd, float pointsToAdd){
        //If a player has an ultimate or has one active buttons are disabled
        if(GSM.movementDistributor.ultimateActive) return false;

        //Add points to respective sides
        if(sideToAdd == buttonSide.red)
            leftPercentage += pointsToAdd;
        else
            rightPercentage += pointsToAdd;

        //Display updated position
        updateBar();

        //Check if a bar is half way to get ultimate
        if(barHalfWay(sideToAdd)){
            clearBar();
            return true;
        }

        return false;
    }

    public void clearBar(){
        //Set bars to default state
        leftPercentage = 0f;
        rightPercentage = 0f;

        updateBar();
    }

    private bool barHalfWay(buttonSide sideToCheck){
        //See if bars are half way
        if(sideToCheck == buttonSide.red){
            if(leftPercentage >= maxBar / 2)
                return true;
        }else{
            if(rightPercentage >= maxBar / 2)
                return true;
        }
        
        return false;
    }
}
