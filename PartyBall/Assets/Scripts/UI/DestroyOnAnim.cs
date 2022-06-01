using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnim : MonoBehaviour
{
    public void destroy(){
        //Used in animators to delete object when the animation is over
        Destroy(gameObject);
    }
}
