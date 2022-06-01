using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingsMenus : MonoBehaviour
{
    public static PlayerSettingsMenus current;

    //Perameters for card spawning info
    [SerializeField] private float cardHeight; //Note: The center of the card relitive to canvas center
    [SerializeField] private float cardPadding; //Space between cards if possible
    private float cardWidth; //The card width found in run-time

    [SerializeField] private GameObject cardPrefab = null; //Prefab of card object
    [SerializeField] private GameObject addPlayerPrefab = null; //Prefab for the add player card
    private List<CardMain> cards = new List<CardMain>(); //All cards spawned in this script

    [SerializeField] private RectTransform cardsTransform; //UI canvas to add cards to

    private int playerCount = 1;
    [SerializeField] private int maxPlayers = 4;

    void Awake(){
        //Make singleton
        current = this;
    }

    void Start(){
        //Make sure there is a card and canvas
        if(cardPrefab == null || cardsTransform == null || addPlayerPrefab == null) Debug.LogError("Variable not assigned in card loader...");

        //Get card width
        cardWidth = cardPrefab.GetComponent<RectTransform>().sizeDelta.x;
        //Note: assumes add player prefab is the same width
    
        //Align cards with one by default
        PlayerPrefs.SetInt("playerCount", playerCount);
        PlayerPrefs.Save();
        alignCards();
    }

    private void alignCards(){
        //Loop for each card
        for(int i = 0; i < playerCount; i++){
            //Create and place with offset
            placeCardAtSlot(i, playerCount < maxPlayers);
        }

        //Remove old cards
        for(int i = 0; i < playerCount - 1; i++){
            GameObject cardToDestroy = cards[0].gameObject;
            cards.RemoveAt(0);
            Destroy(cardToDestroy);
        }

        //Place the card adder
        if(playerCount < maxPlayers){
            placeAddCard();
        }
    }

    private void placeCardAtSlot(int slotNumber, bool offSetForAdder){
            GameObject currentCard = Instantiate(cardPrefab, new Vector3( 0f, 0f, 0f), Quaternion.identity, cardsTransform) as GameObject;
            currentCard.transform.localPosition = calculateCardPos(slotNumber, offSetForAdder);
            
            //Add to collection of cards
            cards.Add(currentCard.GetComponent<CardMain>()); //Note: Would be used for ready up system for multiplayer

            //Set the card number as an identifier
            cards[cards.Count - 1].setCardNumber(slotNumber + 1);
    }

    private Vector3 calculateCardPos(int slotNumber, bool offSetForAdder){
        //Offset calcultation: cardNum * (padding + witdth) - ((cardCount - 1) / (2 * (padding + width)))
        if(offSetForAdder)
            return new Vector3(-((float)playerCount)/2f*(cardPadding + cardWidth) + slotNumber*(cardPadding + cardWidth), cardHeight, 0f);
        else
            return new Vector3(-((float)playerCount - 1f)/2f*(cardPadding + cardWidth) + slotNumber*(cardPadding + cardWidth), cardHeight, 0f);
    }

    private void placeAddCard(){
        //Create addCard and place it in next position
        GameObject addCard = Instantiate(addPlayerPrefab, new Vector3( 0f, 0f, 0f), Quaternion.identity, cardsTransform) as GameObject;
        addCard.transform.localPosition = calculateCardPos(playerCount, true);
    }

    public void increaseCardCount(){
        //Increase card count if possible
        if(playerCount < maxPlayers){
            playerCount++;
            updatePlayerCount();
            alignCards();
        }
    }

    public void decreaseCardCount(){
        //Decrease card count if possible
        if(playerCount > 1){
            playerCount--;
            updatePlayerCount();
            alignCards();
        }
    }

    private void updatePlayerCount(){
        //Set count to count in variable
        PlayerPrefs.SetInt("playerCount", playerCount);
        PlayerPrefs.Save();
    }
}
