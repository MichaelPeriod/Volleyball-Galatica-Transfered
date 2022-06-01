using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public void resetToSpawnLoc(int dirAfter)
    {
        //Reset both ball and player
        GSM.spawnBall.reset(dirAfter);
        GSM.spawnPlayers.reset();
    }
}
