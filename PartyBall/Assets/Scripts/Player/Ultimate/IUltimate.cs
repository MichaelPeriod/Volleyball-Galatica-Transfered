using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

abstract public class IUltimate : MonoBehaviour //Unsure if convention says that it should be an "I" because it is an abstract instead of an interface
{
    protected HandleMovement playerMovementController() => GetComponent<HandleMovement>();
    public Color ultimateColor; //Set in the respective start functions

    public void onShow()
    {
        //Sets a global variable to stop others from getting their ultimate
        GSM.movementDistributor.ultimateActive = true;

        //Set ultimate color
        if (ultimateColor != null) playerMovementController().instantedUltLight.GetComponent<Light2D>().color = ultimateColor;
    }
    public virtual void ultimateTriggered(){
        //On press
        onActivate();

        //Can have "recast" functions here
    }

    public virtual void onActivate(){
        //Preform functionality here

        resetUltimate();
    }

    public virtual void onRecast(){
        //Preform functionality here

        resetUltimate();
    }

    protected void resetUltimate()
    {
        //Set the player's ultimate to false on call
        for(int i = 0; i < GSM.spawnPlayers.spawnedPlayers.Count; i++){
            if(GSM.spawnPlayers.spawnedPlayers[i] == playerMovementController().gameObject){
                playerMovementController().toggleUltimate(i);
            }
        }
    }
}
