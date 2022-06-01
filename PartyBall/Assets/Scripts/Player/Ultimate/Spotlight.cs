using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spotlight : IUltimate
{
    public string lightPosition = "prefabs/shotlight";
    private GameObject sptLight; //Instated light

    private int stage = 1;

    private float timeForceRecast = 7f;


    private void Start()
    {
        //Set ult color
        ultimateColor = new Color(56f / 255f, 236f / 255f, 62f / 255f, 1f);
    }

    public override void ultimateTriggered(){
        //First press and second does diffrent
        switch (stage){
            case 1:
                onActivate();
                break;
            case 2:
                onRecast();
                break;
        }
    }

    public override void onActivate()
    {
        //Make sure ball is not in center
        if(Mathf.Abs(GSM.spawnBall.ball.transform.position.x) <= 1f) return;
        //Make sure ball is on player's side
        if((GSM.spawnBall.ball.transform.position.x <= 0 && playerMovementController().gameObject.transform.position.x >= 0) ||
           (GSM.spawnBall.ball.transform.position.x >= 0 && playerMovementController().gameObject.transform.position.x <= 0)) return;

        //Create light
        sptLight = Instantiate(Resources.Load(lightPosition +
        (playerMovementController().gameObject.transform.position.x >= 0 ? "left" : "right"), typeof(GameObject))) as GameObject;

        //Set ball position
        GSM.spawnBall.freezeBall();
        GSM.spawnBall.setLocation(new Vector3(0f, 2.5f, 0f));

        //Set to ready for recast
        stage++;
        StartCoroutine(setForceRecast(timeForceRecast));
    }

    public override void onRecast()
    {
        //Launch ball
        GSM.spawnBall.ball.GetComponent<Launch>().preformLaunch(0, 23, sptLight.transform.eulerAngles.z + 180);

        //Remove light
        Destroy(sptLight);

        //Reset stage for later recast
        stage = 1;
        GSM.movementDistributor.ultimateActive = false;

        base.onRecast();
    }

    private IEnumerator setForceRecast(float timeToRecast){
        //To avoid players who hold their ult extensively force a shot
        yield return new WaitForSeconds(timeToRecast);

        if(stage == 2) onRecast();
    }
}
