using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerCardCreator : MonoBehaviour {

    public bool createCard;

    public string label;
    public string characterName;
    public string messageText;
    public string[] options;

    public Canvas parentCanvas;
    public PrayerCard templateCard;

    public Vector3 position;
    public Vector2 size;
    public float maxBuffer;

    private Transform parentTransform;

    void Start () {
        parentTransform = parentCanvas.GetComponent<Transform>();

    }

    void CreateCard() {
        Debug.Log("Creating Card");

        PrayerCard newCard = Instantiate(templateCard);
        newCard.label = label;
        GameObject temp = (GameObject) Resources.Load("Characters/" + characterName);
        newCard.character = temp.GetComponent<Character>();
        newCard.messageText = messageText;
        newCard.maxTimeBetweenFrames = 1;

        RectTransform transform = newCard.GetComponent<RectTransform>();
        transform.SetParent(parentTransform, false);

        Vector3 newPos = new Vector3(position.x, position.y, position.z);
        newPos.x += Random.Range(-maxBuffer, maxBuffer);
        newPos.y += Random.Range(-maxBuffer, maxBuffer);
        transform.position = newPos;
        
        transform.sizeDelta = size;
        newCard.gameObject.SetActive(true);
    }
	
	void Update () {
		
        if (createCard) {
            CreateCard();
            createCard = false;
        }

	}
}
