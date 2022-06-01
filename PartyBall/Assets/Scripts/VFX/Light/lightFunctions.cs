using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class lightFunctions : MonoBehaviour
{
    //Instated object
    private Light2D currentLight = null;
    
    //Light peramiters
    private float startingIntensity = 1f;
    private float intensityGoal = 1f;
    private float timeSet = 0f;
    private float transitionTime = .2f;

    void Start()
    {
        //Set light object
        if(currentLight == null) currentLight = GetComponent<Light2D>();
        
        //Set current intensity
        intensityGoal = currentLight.intensity;
    }

    void Update()
    {
        //Linearly interopilate twards any set goal
        currentLight.intensity = Mathf.Lerp(startingIntensity, intensityGoal, Mathf.Clamp((Time.time - timeSet) / transitionTime, 0f, 1f));
    }

    public void setTransition(float _transitionTo, float _transitionTime){
        //Set peramiters for light
        startingIntensity = currentLight.intensity;
        intensityGoal = _transitionTo;
        
        timeSet = Time.time;
        transitionTime = _transitionTime;
    }

    //Add any other light specific functions such as flicker or hue shift if neccisary
}
