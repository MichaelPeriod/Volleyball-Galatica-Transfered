using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelTypes : MonoBehaviour
{
    private void Start()
    {
        //Set count for players to spawn
        GSM.spawnPlayers.playersToSpawn = PlayerPrefs.GetInt("playerCount", 0);

        //Set any player marked as AI in "Load Level"
        for(int i = 1; i <= PlayerPrefs.GetInt("playerCount", 0); i++){
            if(PlayerPrefs.GetInt("P" + i.ToString() + "IsAI") == 1){
                GSM.movementDistributor.controlSchemes[i - 1].isAI = true;
            }
        }

        //Spawn players and set movement once settings is destroyed
        GSM.spawnPlayers.spawnPlayers();
        GSM.movementDistributor.onStart();

        //Load complete
        Destroy(gameObject);
    }
}
