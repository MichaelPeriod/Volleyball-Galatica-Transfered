using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCard : MonoBehaviour
{
    public void onClick(){
        PlayerSettingsMenus.current.increaseCardCount();
        Destroy(gameObject);
    }
}
