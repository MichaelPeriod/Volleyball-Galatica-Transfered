using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualShake : MonoBehaviour
{
    [SerializeField] private float amplitude;
    [SerializeField] private float period;

    public Transform objectToShake = null;

    void Start(){
        //Use custom object or child by default
        if(objectToShake == null) objectToShake = transform.GetChild(0);
    }

    void Update(){
        //Move up and down at interval
        objectToShake.position += Vector3.up * Mathf.Cos(Time.time / period) * amplitude * Time.deltaTime;
    }
}
