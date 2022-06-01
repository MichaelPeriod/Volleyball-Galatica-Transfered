using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LaunchProfile")]
public class LaunchProfile : ScriptableObject
{
    public float delay = 1;
    public float force;
    public float launchAngle;
}
