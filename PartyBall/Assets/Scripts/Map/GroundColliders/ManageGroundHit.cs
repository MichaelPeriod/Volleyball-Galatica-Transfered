using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGroundHit : MonoBehaviour
{
    //Enum to define which team the hit is on
    public enum colSide
    {
        left,
        right
    };

    public void hit(colSide hitSide) // <- called from the "HitGround" script
    {

        //Increase score respectively
        if(hitSide == colSide.right)
            GSM.scoreMgr.leftIncrease();
        else
            GSM.scoreMgr.rightIncrease();

        //Reset player and ball location
        GSM.spawnerController.resetToSpawnLoc((int) hitSide);
    }
}
