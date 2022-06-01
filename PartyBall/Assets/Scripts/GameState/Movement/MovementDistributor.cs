using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementDistributor : MonoBehaviour
{
    //A list of any and all control scemes
    [SerializeField] public List<ControlScheme> controlSchemes;

    //A list of events for when player presses their respective buttons
    [HideInInspector] public List<UnityEvent> onJump = new List<UnityEvent>();
    [HideInInspector] public List<UnityEvent> onUltimate = new List<UnityEvent>();

    //Global variable to show weather an ultimate is currently up
    [HideInInspector] public bool ultimateActive = false;

    public void onStart(){ // <- Called from LoadLevelTypes to replace Start
        //Set as many  controls as there are players
        for(int i = 0; i < GSM.spawnPlayers.playersToSpawn; i++){
            //Create a new "jump event" and assign the player's jump function to it
            onJump.Add(new UnityEvent());
            onJump[i].AddListener(GSM.spawnPlayers.spawnedPlayers[i].GetComponent<HandleMovement>().jumpTriggered);

            onUltimate.Add(new UnityEvent());
            onUltimate[i].AddListener(GSM.spawnPlayers.spawnedPlayers[i].GetComponent<HandleMovement>().ultimatePressed);

            //Assign an AI script to the player prefab if declaired an AI in the loader
            if (controlSchemes[i].isAI)
            {
                AIMain ai = GSM.spawnPlayers.spawnedPlayers[i].AddComponent<AIMain>();
                ai.controller = controlSchemes[i]; //Give it a "virtual controller"
            }

            //Personal ults assigned here
            //Note: ultChoice# gets set in "CardMain"
            switch(PlayerPrefs.GetInt("ultChoice" + (i + 1).ToString())){
                case 1:
                    GSM.spawnPlayers.spawnedPlayers[i].AddComponent<Spotlight>();
                    break;
                case 0:
                    GSM.spawnPlayers.spawnedPlayers[i].AddComponent<Blocker>();
                    break;
                default:
                    Debug.Log("Forgot to assign ultimate");
                    GSM.spawnPlayers.spawnedPlayers[i].AddComponent<Blocker>();
                    break;
            }
        }
    }

    void Update(){
        //Update any input schemes that aren't AI
        for(int i = 0; i < GSM.spawnPlayers.playersToSpawn; i++){
            if (!controlSchemes[i].isAI)
            {
                //Horizontal movement
                controlSchemes[i].horzValue = Input.GetAxisRaw(controlSchemes[i].horizontalAxis);

                //Holding jump
                controlSchemes[i].isDown = Input.GetButton(controlSchemes[i].jumpInput);

                //On jump
                if (Input.GetButtonDown(controlSchemes[i].jumpInput))
                {
                    onJump[i].Invoke();
                }

                //On ultimate
                if (Input.GetButtonDown(controlSchemes[i].ultimateInput))
                {
                    onUltimate[i].Invoke();
                }
            }
        }
    }

    void FixedUpdate(){
        //Pass the info of input to the player
        for(int i = 0; i < GSM.spawnPlayers.playersToSpawn; i++){
            GSM.spawnPlayers.spawnedPlayers[i].GetComponent<HandleMovement>().passThroughInput(controlSchemes[i].horzValue, controlSchemes[i].isDown, controlSchemes[i].hasUltimate);
        }
    }
}