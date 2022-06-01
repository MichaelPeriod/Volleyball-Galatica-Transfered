using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public void Exit(){
        //Quit
        Application.Quit();
        Debug.Log("Quit");
    }
}
