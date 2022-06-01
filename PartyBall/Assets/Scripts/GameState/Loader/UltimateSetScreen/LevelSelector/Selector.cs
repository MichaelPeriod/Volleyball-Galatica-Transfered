using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selector : MonoBehaviour
{
    [SerializeField] private List<LevelInfo> levels = null;
    private List<LevelCardController> cards = new List<LevelCardController>();
    [SerializeField] private GameObject levelCardPrefab = null;
    [SerializeField] private GameObject levelsObject;
    [SerializeField] private float offsetFromSpawner;
    [SerializeField] private float padding;
    private float width;

    void Start(){
        if(levelCardPrefab == null) return;

        width = levelCardPrefab.GetComponent<RectTransform>().sizeDelta.x;

        for(int i = 0; i < levels.Count; i++){
            setLevelCard(i);
        }
    }

    private void setLevelCard(int slotNumber){
        GameObject currCard = Instantiate(levelCardPrefab, Vector3.zero, Quaternion.identity, levelsObject.transform);
        currCard.transform.localPosition = calculateCardPos(slotNumber);

        currCard.GetComponentInChildren<TextMeshProUGUI>().text = levels[slotNumber].levelName;
        currCard.GetComponent<LevelCardController>().onSet(levels[slotNumber].levelNumber);

        cards.Add(currCard.GetComponent<LevelCardController>());
    }

    private Vector3 calculateCardPos(int slotNumber){
        //Offset calcultation: cardNum * (padding + witdth) - ((cardCount - 1) / (2 * (padding + width)))
        return new Vector3(-((float)levels.Count - 1f)/2f*(padding + width) + slotNumber*(padding + width), offsetFromSpawner, 0f);
    }

    public void deselectAllCards(){
        foreach(LevelCardController card in cards){
            card.onDeselect();
        }
    }
}
