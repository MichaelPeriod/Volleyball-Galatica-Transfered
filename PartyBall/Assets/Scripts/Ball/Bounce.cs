using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //Settings for how player interacts with ball
    [Range(0f, 1f)]
    public float bouncePointBias = .5f;
    public float playerBounceFactor = 1.5f;

    //Settings that decide how fast a bounce can be
    public const float ballMaxSpeed = 13f;
    public const float ballMinSpeed = 3f;

    //OnHit particle system prefabs
    [SerializeField] private GameObject onHitParticles;

    private Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    //Happens after physics is calculated
    public void OnCollisionEnter2D(Collision2D col){
        bounce(col);
    }

    private void bounce(Collision2D col){
        //Find the magnitude of velocity
        float currentSpeed = Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2));

        //Location where ball hits
        Vector2 hitPoint = col.collider.ClosestPoint(transform.position);

        //Get vector of direction of perpendicular bounce and realistic bounce
        Vector2 bounceDiffrence = ((Vector2) transform.position - hitPoint).normalized;
            
        Vector2 realisticAngle = rb.velocity.normalized;

        Vector2 launchAngle = new Vector2(0f, 0f);

        if(col.gameObject.tag == "Player"){
            //Only take bounce point into account if player
            Vector2 diffrenceInCenters = (Vector2) (transform.position - col.transform.position); 

            launchAngle = (diffrenceInCenters * bouncePointBias + realisticAngle * (1 - bouncePointBias));
            
            //Use a variable speed to add to ball speed
            currentSpeed += Mathf.Sqrt(ballMaxSpeed-Mathf.Clamp(currentSpeed, ballMinSpeed, ballMaxSpeed))/playerBounceFactor - 1f;

            //Add particles
            addHitPuff(col.GetContact(0).point);
        } else {
            launchAngle = realisticAngle;
        }

        //Set new launch angle
        rb.velocity = launchAngle * Mathf.Clamp(currentSpeed, ballMinSpeed, ballMaxSpeed);
    }

    private void addHitPuff(Vector2 hitPos){
        Instantiate(onHitParticles, (Vector3) hitPos, Quaternion.identity);
    }
}
