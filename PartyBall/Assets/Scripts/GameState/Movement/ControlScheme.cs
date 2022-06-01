using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public class ControlScheme
{
    //Decided before instating player objects
    public bool isAI = false;

    //The string identifiers the computer uses for retrieving input
    public string horizontalAxis;
    public string jumpInput;
    public string ultimateInput;

    //The end output of the player
    [HideInInspector]
    public float horzValue;
    [HideInInspector]
    public bool isDown;
    [HideInInspector]
    public bool hasUltimate;
}