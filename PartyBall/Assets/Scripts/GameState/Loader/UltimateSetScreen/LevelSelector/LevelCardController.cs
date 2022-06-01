using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardController : MonoBehaviour
{
    private int levelNumber;

    public void onSet(int levelNum){
        levelNumber = levelNum;
    }

    public void onSelect(){
        PlayerPrefs.SetInt("queuedLevel", levelNumber);
        PlayerPrefs.Save();

        GetComponentInParent<Selector>().deselectAllCards();
        GetComponent<Image>().enabled = true;
    }

    public void onDeselect(){
        GetComponent<Image>().enabled = false;
    }
}
