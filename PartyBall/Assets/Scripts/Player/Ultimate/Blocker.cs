using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : IUltimate
{
    //AKA Shield but ran into naming convention problems
    public string shiledPrefabPosition = "prefabs/shield";

    private void Start()
    {
        //A blueish/cyan color
        ultimateColor = new Color(56f / 255f, 161f / 255f, 236f / 255f, 1f);
    }

    public override void onActivate(){
        //Make sure ball is not in center
        if(Mathf.Abs(GSM.spawnBall.ball.transform.position.x) <= 1f) return;
        //Make sure ball is on oponent's side
        if((GSM.spawnBall.ball.transform.position.x >= 0 && playerMovementController().gameObject.transform.position.x >= 0) ||
           (GSM.spawnBall.ball.transform.position.x <= 0 && playerMovementController().gameObject.transform.position.x <= 0)) return;

        //Create the shield object
        Instantiate(Resources.Load(shiledPrefabPosition, typeof(GameObject)));

        //Set ultimate to off
        base.onActivate();
    }
}
