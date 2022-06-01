using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IUTextColorChanger : MonoBehaviour
{
    //Instated game object
    [SerializeField] private TextMeshProUGUI text = null;

    //Properties of color changing
    [SerializeField] private float timeAtRest;
    [SerializeField] private float lerpTime;
    private float timeColorSet;

    //Colors
    private Color currentColor;
    private Color nextColor;
    void Start(){
        //Set text if not already set
        if(text == null) text = GetComponent<TextMeshProUGUI>();

        //Start the color change cycle
        if(text != null) StartCoroutine(randomizeColor());
    }

    void Update(){
        //Update color to linearly interpolate to next color
        text.color = Color32.Lerp(currentColor, nextColor, Mathf.Clamp((Time.time - timeColorSet) / lerpTime, 0f, 1f));
    }

    private IEnumerator randomizeColor(){
        //Continue loop
        while(true){
            //Hold last color
            currentColor = text.color;

            //Set values for next color
            nextColor = new Color(
                Random.value,
                Random.value,
                Random.value,
                255
            );

            //Set new starting time
            timeColorSet = Time.time;

            //Set timer to go again after transition and rest
            yield return new WaitForSeconds(timeAtRest + lerpTime);
        }
    }
}
