using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CardMain : MonoBehaviour
{
    //Text that refers to the top of the card which says "Player #"
    [SerializeField] private TextMeshProUGUI playerNumberText;
    //Sub-text that is only visible if PlayerPrefs.GetInt("P#IsAI") == true
    [SerializeField] private GameObject isAIText;
    
    //Sliders that hold current slected player type and ultimate
    [SerializeField] private SliderImageManager playerIconSlider;
    [SerializeField] private SliderImageManager ultimateSlider;

    //Refers to the number imported from "PlayerSettingsMenus" from the loader object
    private int cardNumber;

    
    void Start(){
        //Call functions when the slider is changed to update current selection
        if(playerIconSlider != null) playerIconSlider.iconChanged.AddListener(onPlayerChange);
        if(ultimateSlider != null) ultimateSlider.iconChanged.AddListener(onUltimateChange);
    }

    public void setCardNumber(int _number){ // <- Called from "PlayerSettingsMenus" on the loader game object
        //Set card number
        cardNumber = _number;

        //Set to not AI by default
        PlayerPrefs.SetInt("P" + cardNumber.ToString() + "IsAI", 0);
        PlayerPrefs.Save();

        //Set player num text and checks if the AI text should be shown
        playerNumberText.text = "PLAYER " + _number.ToString();
        displayAIText();

        //Save the default state of choices
        onPlayerChange();
        onUltimateChange();
    }

    private void displayAIText(){
        if(PlayerPrefs.GetInt("P" + cardNumber.ToString() + "IsAI") == 1)
            isAIText.SetActive(true);
        else
            isAIText.SetActive(false);
    }

    private void onPlayerChange(){
        //Gets the current slot number
        int currentImageNumber = playerIconSlider.currentImage;

        //Saves the slot number
        PlayerPrefs.SetInt("PlayerIcon" + cardNumber.ToString(), currentImageNumber);
        PlayerPrefs.Save();
    }

    private void onUltimateChange(){
        //Get current slot number
        int currentImageNumber = ultimateSlider.currentImage;

        //Saves the slot number
        PlayerPrefs.SetInt("ultChoice" + cardNumber.ToString(), currentImageNumber);
        PlayerPrefs.Save();
    }

    public void toggleAI(){
        string aiText = "P" + cardNumber.ToString() + "IsAI";

        PlayerPrefs.SetInt(aiText, PlayerPrefs.GetInt(aiText) == 1 ? 0 : 1);
        PlayerPrefs.Save();

        displayAIText();
    }
}
