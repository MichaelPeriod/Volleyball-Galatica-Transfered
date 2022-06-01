using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    //Ball properties
    public Transform ballSpawnLocation;
    [SerializeField] private GameObject ballPrefab = null;
    public GameObject ball;

    //Ball direction indicator
    public GameObject arrow;
    public Vector3 leftArrowPos;
    public GameObject arrowObj;

    private void Start()
    {
        //Create ball and prep it to send
        if(ballPrefab != null)
        {
            ball = Instantiate(ballPrefab, ballSpawnLocation.position, Quaternion.identity);
            reset();
        }
    }

    public void reset(int dirToLaunch = 1)
    {
        //Reset ball to standstill at prefered location
        setLocation(ballSpawnLocation.transform.position);
        freezeBall();

        //Launch the ball
        ball.GetComponent<Launch>().preformLaunch(dirToLaunch);

        //Create arrow to play animation in correct direction
        arrowObj = Instantiate(arrow, leftArrowPos, Quaternion.identity);
        if(dirToLaunch == (int) ManageGroundHit.colSide.left){
            arrowObj.transform.position = new Vector3(arrowObj.transform.position.x * -1, arrowObj.transform.position.y, 0f);
            Vector3 tempLS = new Vector3(arrowObj.transform.localScale.x * -1, arrowObj.transform.localScale.y, 1f);
            arrowObj.transform.localScale = tempLS;
        }
    }

    public void setLocation(Vector3 position){
        ball.transform.position = position;
        ball.transform.rotation = ballSpawnLocation.transform.rotation;
    }

    public void freezeBall(){
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
