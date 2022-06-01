using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayGameOutcome : MonoBehaviour
{
    //Instated object
    [SerializeField] private TextMeshProUGUI textObject = null;

    //Text display types
    private enum outcomeDisplay {
        gameOverText,
        scoreText
    };
    [SerializeField] private outcomeDisplay textType;

    void Start(){
        //Set text object if not already
        if(textObject == null) textObject = GetComponent<TextMeshProUGUI>();

        //Decide text
        string textToDisplay = "";
        switch(textType){
            case outcomeDisplay.gameOverText:
                textToDisplay = gameOverText();
                break;
            case outcomeDisplay.scoreText:
                textToDisplay = scoreText();
                break;
            default:
                Debug.Log("Invalid text type...");
                break;
        }

        //Set text
        textObject.text = textToDisplay;
    }

    private string gameOverText(){
        string outputText = "GAME OVER\n";
        if(PlayerPrefs.GetInt("TimeLeft") == 0){
            if(PlayerPrefs.GetString("SideWon") == "Left"){ //Left by time
                outputText += "LEFT";
            } else if (PlayerPrefs.GetString("SideWon") == "Right"){ //Right by time
                outputText += "RIGHT";
            } else { //Tie by time
                outputText += "NO-ONE";
            }
            outputText += " WON BECAUSE THE TIMER RAN OUT";
        } else {
            if(PlayerPrefs.GetString("SideWon") == "Left"){ //Left by points
                outputText += "LEFT";
            } else { //Right by points
                outputText += "RIGHT";
            }
            outputText += " WON BY POINTS";
        }

        return outputText;
    }

    private string scoreText(){
        string outputText = "FINAL SCORE\n";

        outputText += PlayerPrefs.GetInt("LeftScore", 0).ToString();
        outputText += "   TO   ";
        outputText += PlayerPrefs.GetInt("RightScore", 0).ToString();

        return outputText;
    }
}
