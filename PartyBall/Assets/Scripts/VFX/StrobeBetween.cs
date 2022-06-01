using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrobeBetween : MonoBehaviour
{
    //Instated rotatable object
    [SerializeField] private Transform objectToRotate;

    //Perameters that define object's functionality
    [SerializeField] private float deltaAngle;
    [SerializeField] private float transitionTime;
    [SerializeField] private float restTime;

    //Used to keep track how long since start
    private float startTime;

    //Manages positive and negitive direction
    [SerializeField] private bool isGoingForwards = true;
    private bool hasSwitched = true;

    void Start(){
        //Sets current object as subject if not currently set
        if(objectToRotate == null) objectToRotate = transform;
        
        startTime = Time.time;
    }

    void Update(){
        //Calculate how long since last cycle
        float relitiveTime = (Time.time - startTime) % (transitionTime + restTime);

        if(relitiveTime <= transitionTime){ //Object has yet to meet far angle
            hasSwitched = false;
            
            //Set clockwise if going forwards
            if(isGoingForwards)
                objectToRotate.Rotate(0f, 0f, (deltaAngle/transitionTime) * Time.deltaTime);
            else
                objectToRotate.Rotate(0f, 0f, -(deltaAngle/transitionTime) * Time.deltaTime);
        } else {
            //Set opposite direction after hitting a bound
            if(!hasSwitched){
                if(isGoingForwards)
                    isGoingForwards = false;
                else
                    isGoingForwards = true;
                
                hasSwitched = true;
            }
        }
    }
}
