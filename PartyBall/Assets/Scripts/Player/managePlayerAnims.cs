using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managePlayerAnims : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    void Start(){
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        //Update peramiters based on current player state
        isRunning();
        isJumping();
    }

    private void isRunning(){
        //See if moving sideways
        if(Mathf.Abs(rb.velocity.x) >= 0.1f){
            animator.SetBool("walking", true);
            
            //Flip player sprite when going left
            if(rb.velocity.x > 0){
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            } else if(rb.velocity.x < 0){
                gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        } else {
            animator.SetBool("walking", false);
        }
    }

    private void isJumping(){
        //See if moving vertically
        if(Mathf.Abs(rb.velocity.y) >= 0.1f){
            animator.SetBool("onGround", false);
        } else {
            animator.SetBool("onGround", true);
        }
    }
}
