using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGFXChanger : MonoBehaviour
{
    //Listed where number on shield is index + 1
    public List<Sprite> visualDamageIndicators;

    public void setImage(int hitCountLeft){
        //Change image based on health
        if(hitCountLeft > 0)
            GetComponent<SpriteRenderer>().sprite = visualDamageIndicators[hitCountLeft - 1];
    }
}
