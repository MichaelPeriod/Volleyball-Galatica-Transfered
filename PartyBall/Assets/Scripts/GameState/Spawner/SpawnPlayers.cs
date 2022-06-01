using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    //Passed in though the loader
    public int playersToSpawn;

    public List<Transform> idealSpawningLocations;

    public List<RuntimeAnimatorController> animationControllers = null;
    
    public List<GameObject> spawnedPlayers = new List<GameObject>();
    
    public GameObject playerPrefab = null;

    public void spawnPlayers() // <- Called from LoadLevelTypes to replace start function
    {
        //Create a player at all locations possible up to players to spawn threashhold
        if(playerPrefab != null && idealSpawningLocations.Count >= playersToSpawn)
        {
            for(int spawned = 0; spawned < playersToSpawn; spawned++)
            {
                //Spawn the player
                spawnedPlayers.Add(Instantiate(playerPrefab, idealSpawningLocations[spawned].position, Quaternion.identity));

                //Set animation controller based on "CardMain"'s saved PlayerIcon#
                if(animationControllers != null)
                    spawnedPlayers[spawned].GetComponent<Animator>().runtimeAnimatorController =
                                animationControllers[PlayerPrefs.GetInt("PlayerIcon" + (spawned + 1).ToString())];
            }
        }
    }

    public void reset()
    {
        //Set all player positions to default
        for (int i = 0; i < playersToSpawn; i++)
        {
            spawnedPlayers[i].transform.position = idealSpawningLocations[i].transform.position;
            spawnedPlayers[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
