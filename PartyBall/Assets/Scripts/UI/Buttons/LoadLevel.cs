using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    //Maybe make top two static functions?
    public void loadByNumber(int level){
        SceneManager.LoadScene(level);
    }

    public void loadNext(){
        loadByNumber(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void loadQueued(){
        loadByNumber(PlayerPrefs.GetInt("queuedLevel", 0));
    }

    public void loadLast(){
        loadByNumber(SceneManager.sceneCountInBuildSettings - 1);
    }
}
