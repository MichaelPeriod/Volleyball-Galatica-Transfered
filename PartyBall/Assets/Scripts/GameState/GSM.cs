using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GSM : MonoBehaviour
{
    //All scripts
    public static GSM current;
    public static SpawnerController spawnerController;
    public static SpawnBall spawnBall;
    public static SpawnPlayers spawnPlayers;
    public static MovementDistributor movementDistributor;
    public static ScoreManager scoreMgr;
    public static ScoreDistributor scoreDistributor;
    public static GameClock clock;

    //All objects the scripts come from
    public GameObject Spawner = null;
    public GameObject Movement = null;
    public GameObject Score = null;
    public GameObject globalLight = null;

    void Awake(){
        current = this;
    }

    void OnEnable(){
        //Set scripts from each object
        if(Spawner != null){
            spawnerController = Spawner.GetComponent<SpawnerController>();
            spawnBall = Spawner.GetComponent<SpawnBall>();
            spawnPlayers = Spawner.GetComponent<SpawnPlayers>();
        }

        if(Movement != null){
            movementDistributor = Movement.GetComponent<MovementDistributor>();
        }

        if(Score != null){
            scoreMgr = Score.GetComponent<ScoreManager>();
            scoreDistributor = Score.GetComponent<ScoreDistributor>();
            clock = Score.GetComponent<GameClock>();
        }
    }
}
