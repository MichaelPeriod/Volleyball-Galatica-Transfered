using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMain : MonoBehaviour
{
    /*
    AI need to do's:
    Must be able to hit the ball with moderate comprehension over the center wall.

    AI needs:
    Knowledge of personal controls, V
    ball position,
    prediction of where ball is going,
    knowlage of how to hit ball to get desired angle,
    know how to get to ball predicted position

    Steps:
    1. Make script that allows interaction between ai and player object
    2. Make prediction of ball location where it will hit ground
    3. Move player to hit position
    4. Set ideal hit angle
    5. Hardcode diffrent "profiles" of hit to allow hitting in ways like vertical, 45 degree, ect.
    6. Profit
     */

    // t = -ballGravity()/2 * t^2 + (ballVel().y * Mathf.Sin(Mathf.Atan(ballVel().y/ballVel().x)))t + (ballPos().y - currentSize().y)

    // xpos += xv0 * t

    //AI settings
    public ControlScheme controller;// <- injected by MovementDistributor
    public float dificulty = 1.6f;
    private GameObject predictionPoint;
    [Range(-1,1)][SerializeField] private float horzAxis = 0; //Virtual controller

    //Knows of external sources
    private float ballGravity() => GSM.spawnBall.ball.GetComponent<Rigidbody2D>().gravityScale;
    private Vector2 currentSize() => gameObject.transform.localScale;
    private Vector2 ballPos() => GSM.spawnBall.ball.transform.position;
    private Vector2 ballVel() => GSM.spawnBall.ball.GetComponent<Rigidbody2D>().velocity;
    private float precision;

    void Start(){
        precision = currentSize().x/dificulty;

        //If prediction is enabled show the prediction
        predictionPoint = GameObject.Find("AIPrediction");
    }

    private void Update()
    {
        //Take a prediction of the ball and set destination where the controld are applied accordingly
        movePredictedDirection(predictionOfBall());
        updateControls();
    }

    private void updateControls()
    {
        //Set the "Virtual controller" to the prediction
        controller.horzValue = horzAxis;

        //Automaticly use ult if AI has it
        if(controller.hasUltimate){
            for(int i = 0; i < GSM.movementDistributor.controlSchemes.Count; i++){
                if(GSM.movementDistributor.controlSchemes[i] == controller){
                    GSM.movementDistributor.onUltimate[i].Invoke();
                }
            }
        }
    }

    private void movePredictedDirection(float xPrediction){
        //Check if prediction is close enough
        if(Mathf.Abs(gameObject.transform.position.x - xPrediction) > precision){
            horzAxis = Mathf.Clamp(xPrediction - gameObject.transform.position.x, -1, 1);
        }
    }

    private float predictionOfBall(){
        //Use a quadratic to solve for time
        float timeTilLowEnough = quadratic(-ballGravity()/2, ballVel().y, ballPos().y - currentSize().y + 4);

        //Assuming constant velocity, go in the direction
        float predictedXPos = ballPos().x + ballVel().x * timeTilLowEnough;

        //Update prediction
        if(predictionPoint != null)
            predictionPoint.transform.position = new Vector2(predictedXPos, -4 + currentSize().y);
    
        return predictedXPos;
    }

    private float quadratic(float a, float b, float c){
        //Take positive and negitive and only return positive due to it being time
        float posQdr = (-b + (Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c)))/(2*a);
        float negQdr = (-b - (Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c)))/(2*a);

        if(posQdr >= 0)
            return posQdr;
        else if(negQdr >= 0)
            return negQdr;
        
        return 0;
    }
}
