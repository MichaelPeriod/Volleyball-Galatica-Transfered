using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    private float defaultGravityScale;

    private Rigidbody2D rb;

    public List<LaunchProfile> profiles = null;

    void OnEnable(){
        rb = GetComponent<Rigidbody2D>();
        
        //Save gravity for if the ball gets frozen before launch
        defaultGravityScale = rb.gravityScale;
    }

    public void preformLaunch(int profile = 1)
    {
        StartCoroutine(delayLaunch(profiles[profile].delay, profiles[profile].force, profiles[profile].launchAngle));
    }

    public void preformLaunch(float delay, float force, float launchAngle){
        StartCoroutine(delayLaunch(delay, force, launchAngle));
    }

    private IEnumerator delayLaunch(float seconds, float speed, float angle){
        //Stop ball from falling for launch
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(seconds);

        //Exicute launch and re-aply gravity
        launch(speed, angle);
        rb.gravityScale = defaultGravityScale;
    }

    private void launch(float speed, float angle){
        //Ensure force will be valid
        rb.constraints = RigidbodyConstraints2D.None;

        //Add force at an angle
        rb.AddForce(speed * new Vector2(Mathf.Cos((angle - 90) * Mathf.PI/ 180), Mathf.Sin((angle - 90) * Mathf.PI / 180)), ForceMode2D.Impulse);
    }
}
