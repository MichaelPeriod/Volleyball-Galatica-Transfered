using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGround : MonoBehaviour
{
    [SerializeField] private ManageGroundHit.colSide colSide;
    public void OnTriggerEnter2D(Collider2D col)
    {
        //Send hit info to ManageGroundHit in the "GroundTriggers" object
        if(col.tag == "Ball")
            transform.parent.GetComponent<ManageGroundHit>().hit(colSide);
    }
}
