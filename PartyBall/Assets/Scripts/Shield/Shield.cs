using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int hitsLeft = 3;

    void Start(){
        //Set to delete if scored on
        GSM.scoreMgr.leftScoreIncrease.AddListener(destoryOnScore);
        GSM.scoreMgr.rightScoreIncrease.AddListener(destoryOnScore);
        GSM.scoreMgr.resetScores.AddListener(destoryOnScore);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Reduce shield helth on hit
        if (collision.collider.tag == "Ball"){
            hitsLeft--;
            gameObject.GetComponentInChildren<ShieldGFXChanger>().setImage(hitsLeft);
        }

        //Destroy if out of helth
        if (hitsLeft <= 0)
            destroySelf();
    }

    private void destoryOnScore(){ //Called from listerners set in start function
        destroySelf();
    }

    private void destroySelf(){
        GSM.movementDistributor.ultimateActive = false;
        Destroy(gameObject);
    }
}
