using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SliderImageManager : MonoBehaviour
{
    //Sprite object
    [SerializeField] private Image currentSprite;
    
    //A list of sprites in order
    [SerializeField] private List<Sprite> spriteOptions = null;
    //The image number that is displayed by currentSprite
    [HideInInspector] public int currentImage = 0;

    //Event for any time the icon is changed
    public UnityEvent iconChanged;

    //Update the sprite shown
    private void updateSprite() => currentSprite.sprite = spriteOptions[currentImage];
    void Start(){
        //Make sure there are sprites in the list
        if(spriteOptions == null){
            Debug.LogError("No sprites set..."); //Haven't tested but should show an error
            return;
        }

        //Align sprite with current cursor position
        currentSprite.sprite = spriteOptions[currentImage];

        //Make sprite update every time the cursor changes position
        iconChanged.AddListener(updateSprite);
    }

    //Note: May add animation after pressing arrows on side
    public void nextSprite(){
        //Add one to cursor position and loop if out of bounds
        if(spriteOptions.Count > currentImage + 1)
            currentImage++;
        else
            currentImage = 0;
        
        //Update icon
        iconChanged.Invoke();
    }

    public void previousSprite(){
        //Remove one to cursor position and loop if out of bounds
        if(0 < currentImage)
            currentImage--;
        else
            currentImage = spriteOptions.Count - 1;
        
        //Update icon
        iconChanged.Invoke();
    }
}
