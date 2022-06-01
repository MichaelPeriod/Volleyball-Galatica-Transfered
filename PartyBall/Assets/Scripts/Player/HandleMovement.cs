using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMovement : MonoBehaviour
{
    /*Note: Thinking about dividing into seporate scripts:
    MovementMain
    Walking
    JumpAndWallJump
    Ultimate
    */
    
    [Header("--Movement--")]
    [Range(0f, 5f)]
    public float speed = 5f;
    [Range(0f, 20f)]
    public float maxSpeed = 5f;


    [Header("--Jump--")]
    [Range(10f, 30f)]
    public float jumpHeight = 3f;
    [Range(0f, 10f)]
    public float fallMultiplier = 2.5f;
    [Range(0f, 10f)]
    public float lowJumpMultiplier = 2f;
    [Range(0f, 1f)]
    public float jumpForgivance = 0.01f;


    [Header("--Wall Jump--")]
    [Range(10f, 20f)]
    public float wallJumpForce = 11.5f;
    [Range(0f, 90f)]
    public float wallJumpLaunchAngle = 45f;
    [Range(0f, 1f)]
    public float wallJumpForgiveness = 0.1f;   
    private bool rightJumpableWall = true;
    private bool lauched = false;


    [Header("--Ultimate--")]
    [SerializeField] private GameObject ultimateLight;
    [HideInInspector] public GameObject instantedUltLight;
    
    public bool hasUltimate;
    private IUltimate currentUltimate() => GetComponent<IUltimate>();

    private Rigidbody2D rb;

    public void OnEnable()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        //Convert to radians
        wallJumpLaunchAngle *= Mathf.PI / 180;
        
        //Change collision from player bounds
        jumpForgivance += transform.localScale.y / 2;
        wallJumpForgiveness += transform.localScale.x / 2;
    }

    public void passThroughInput(float horzValue, bool holdingJump, bool _hasUltimate)
    {
        //Side to side movement
        if(!lauched){
            move(horzValue);
        }

        //Falling
        fall(holdingJump);

        //Set ult
        hasUltimate = _hasUltimate;
    }

    public void jumpTriggered(){
        if(!lauched){
            //Jumping
            if (checkDown() <= jumpForgivance) {
                jump();
            } else if (checkWalls()){ //If walls close enough then wall jump
                wallJump();
            }
        }
    }

    public void ultimatePressed(){
        //Trigger ultimate
        if(hasUltimate){
            if(currentUltimate() != null)
                currentUltimate().ultimateTriggered();
        }
    }

    private void move(float dir)
    {
        //Add horizontal force
        rb.AddForce(new Vector2(dir * speed, 0f), ForceMode2D.Impulse);

        //Enforce maximum to speed
        if(Mathf.Abs(rb.velocity.x) > maxSpeed){
            rb.velocity = new Vector2(rb.velocity.normalized.x * maxSpeed, rb.velocity.y);
        }
    }

    private void jump()
    {
        //Adds impulse up
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
    }

    private float checkDown()
    {
        float lowestDist = 9999;
        //Send ray down
        RaycastHit2D[] downHit = Physics2D.RaycastAll(transform.position, Vector2.down);

        //Check nearest object bellow
        foreach (RaycastHit2D hit in downHit)
        {
            if (hit.transform != transform)
            {
                if (Mathf.Abs(transform.position.y - hit.point.y) < lowestDist)
                    lowestDist = Mathf.Abs(transform.position.y - hit.point.y);
            }
        }

        return lowestDist;
    }

    private void wallJump()
    {
        //Reset fall
        rb.velocity = rb.velocity - new Vector2(0f, rb.velocity.y);

        //Jump off nearest wall
        if (rightJumpableWall)
            rb.AddForce(new Vector2(-1 * wallJumpForce * Mathf.Cos(wallJumpLaunchAngle), 1 * wallJumpForce * Mathf.Sin(wallJumpLaunchAngle)), ForceMode2D.Impulse);
        else
            rb.AddForce(new Vector2(wallJumpForce * Mathf.Cos(wallJumpLaunchAngle), 1 * wallJumpForce * Mathf.Sin(wallJumpLaunchAngle)), ForceMode2D.Impulse);
    
        lauched = true;
    }

    private bool checkWalls()
    {
        //Send rays both directions
        RaycastHit2D[] rightHit = Physics2D.RaycastAll(transform.position, Vector2.right);
        RaycastHit2D[] leftHit = Physics2D.RaycastAll(transform.position, Vector2.left);

        //Check if right nearest object is close enough
        foreach (RaycastHit2D hit in rightHit)
        {
            if (hit.transform != transform)
            {
                if (Mathf.Abs(hit.point.x - transform.position.x) <= wallJumpForgiveness)
                {
                    rightJumpableWall = true;
                    return true;
                }
            }
        }

        //Check if left nearest object is close enough
        foreach (RaycastHit2D hit in leftHit)
        {
            if (hit.transform != transform)
            {
                if (Mathf.Abs(hit.point.x - transform.position.x) <= wallJumpForgiveness)
                {
                    rightJumpableWall = false;
                    return true;
                }
            }
        }

        //Walls are not close enough
        return false;
    }

    private void fall(bool holdingJump)
    {
        //Adapted from Board to Bits Games
        //https://www.youtube.com/watch?v=7KiK0Aqtmzc

        //Add higher gravity to allow longer jumps by holding
        if ((rb.velocity.y < 0 && holdingJump) || lauched){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        } else if (rb.velocity.y != 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        //Reset dive state
        if(rb.velocity.y <= 0 && checkDown() <= jumpForgivance)
            if(lauched)
                lauched = false;
    }

    public void toggleUltimate(int playerNumber){
        if(!GSM.movementDistributor.controlSchemes[playerNumber].hasUltimate){ //Enable ultimate
            //Set backend
            GSM.movementDistributor.controlSchemes[playerNumber].hasUltimate = true;

            //Show visuals
            GSM.current.globalLight.GetComponent<lightFunctions>().setTransition(0.7f, 0.4f);
            instantedUltLight = Instantiate(ultimateLight, transform);
            GetComponent<IUltimate>().onShow();
        } else { //Disable ultimate
            //Set backend
            GSM.movementDistributor.controlSchemes[playerNumber].hasUltimate = false;

            //Remove visuals
            Destroy(instantedUltLight);

            //Only change global light if no other player has ultimate
            bool removeLight = true;
            foreach(ControlScheme player in GSM.movementDistributor.controlSchemes){
                if(player.hasUltimate){
                    removeLight = false;
                    break;
                }
            }

            //Return light to full
            if(removeLight) GSM.current.globalLight.GetComponent<lightFunctions>().setTransition(1f, 0.4f);
        }
    }

    public int getPlayerNumber(){
        for(int i = 0; i < GSM.spawnPlayers.playersToSpawn; i++){
            if(GSM.spawnPlayers.spawnedPlayers[i] == gameObject){
                return i;
            }
        }

        return 0;
    }
}