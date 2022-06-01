using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    public Transform background = null;
    
    void Start(){
        //Use custom object or child by default
        if(background == null) background = transform.GetChild(0);
    }

    void Update(){
        //Move horizontally over time at fixed rate
        background.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
    }
}
