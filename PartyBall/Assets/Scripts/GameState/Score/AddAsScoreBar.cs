using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAsScoreBar : MonoBehaviour
{
    //The "Bar" image
    private Image icon;

    public enum barSide{
        left,
        right
    };

    //Bar and place
    public barSide side;
    public int barNumber;

    void Start(){
        icon = gameObject.GetComponent<Image>();

        //Add to the colletive in it's slot
        GSM.scoreDistributor.addToPoints(gameObject, barNumber, side);
    }

    //Change the color
    public void setColor(Color clr){
        icon.color = clr;
    }
}
