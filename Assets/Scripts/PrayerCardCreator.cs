using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PrayerCardCreator : MonoBehaviour {

    public bool createCard;

    public string label;
    public string characterName;
    public string messageText;
    public string[] options;

    public Canvas parentCanvas;
    public PrayerCard templateCard;
    public Button responseButton;

    public Vector3 position;
    public Vector2 size;
    public float sizePerButton;
    public float maxBuffer;

    private Transform parentTransform;

    void Start () {
        parentTransform = parentCanvas.GetComponent<Transform>();
    }

    public void CreateCard(string newLabel, string newCharacterName, string newMessageText, string[] newOptions) {
        Debug.Log("Creating Card: " + newLabel);

        PrayerCard newCard = Instantiate(templateCard);
        newCard.label = newLabel;
        newCard.messageText = newMessageText;
        newCard.maxTimeBetweenFrames = 1;
        newCard.responseButton = responseButton;
        newCard.options = newOptions;

        GameObject temp = (GameObject)Resources.Load("Characters/" + newCharacterName);
        newCard.character = temp.GetComponent<Character>();

        RectTransform transform = newCard.GetComponent<RectTransform>();
        transform.SetParent(parentTransform, false);

        Vector3 newPos = new Vector3(position.x, position.y, position.z);
        newPos.x += Random.Range(-maxBuffer, maxBuffer);
        newPos.y += Random.Range(-maxBuffer, maxBuffer);
        transform.position = newPos;

        Vector2 newSize = new Vector2(size.x, size.y);
        newSize.y += (sizePerButton * newOptions.Length);
        transform.sizeDelta = newSize;

        newCard.gameObject.SetActive(true);
    }

    void CreateCard() {
        Debug.Log("Called editor constructor.");
        this.CreateCard(label, characterName, messageText, options);
    }
	
	void Update () {
		
        if (createCard) {
            CreateCard();
            createCard = false;
        }

	}
}
