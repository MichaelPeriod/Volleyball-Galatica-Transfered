using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltButtons : MonoBehaviour
{
    //Button info
    [SerializeField] private DisplayProgress.buttonSide buttonSide;
    [SerializeField] private float deltaPrecentPerSecond; //Increase in bar per second
    
    private bool buttonDown = false;
    private bool ButtonDown {
        get{
            return buttonDown;
        }

        set{
            //Set like this to sync animations
            buttonDown = value;
            GetComponent<Animator>().SetBool("isPressed", buttonDown);
        }
    }

    //Bar that controls visual progress
    [SerializeField] private DisplayProgress progressBar;

    //Used to give ult to player who peressed it
    private GameObject lastTriggerer;

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            //Button is being pressed down
            lastTriggerer = col.gameObject;

            if(!lastTriggerer.GetComponent<HandleMovement>().hasUltimate)
                ButtonDown = true;
        }
    }

    void OnTriggerExit2D(Collider2D col){
        //Button has been relesed
        if(col.tag == "Player")
            ButtonDown = false;
    }

    void Update(){
        //If the button is down add points for the respective side
        //If the player hits the threshhold for ultimate and doesn't already have one then activate it
        if(ButtonDown)
            if(progressBar.addPoints(buttonSide, deltaPrecentPerSecond * Time.deltaTime)) //Also checks if ultimate threshold is met
                if(!lastTriggerer.GetComponent<HandleMovement>().hasUltimate){
                    ButtonDown = false;
                    lastTriggerer.GetComponent<HandleMovement>().toggleUltimate(lastTriggerer.GetComponent<HandleMovement>().getPlayerNumber());
                }
    }
}
