using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSelfDestruct : MonoBehaviour
{
    //Get info on thing to destory
    [SerializeField] private GameObject subject = null;
    [SerializeField] private bool primeOnStart = true;
    [SerializeField] private float explosionTimer;
    
    void Start(){
        //Set object to self if not set
        if(subject == null) subject = gameObject;

        //Set primed if prime on start
        if(primeOnStart)
            StartCoroutine(destroyAfterSeconds(explosionTimer));
    }

    public IEnumerator destroyAfterSeconds(float timer){
        //Wait for timer
        yield return new WaitForSeconds(timer);

        //Delete self
        Destroy(subject);
    }

    public void prime(float timer){
        //Do not set if set by default
        if(primeOnStart) return;

        //Set primed
        StartCoroutine(destroyAfterSeconds(timer));
    }
}
