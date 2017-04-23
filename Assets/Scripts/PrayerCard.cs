using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PrayerCard : MonoBehaviour {

    public string label;
    public Character character;
    public string messageText;

    public Button responseButton;
    public string[] options;

    public float minTimeBetweenFrames = 0;
    public float maxTimeBetweenFrames = 1;

    private float thisTime;
    private float elapsedTime;

    private Transform header;
    private Transform idBox;
    private Transform idPhoto;
    private Transform profileText;
    private Transform message;
    private Transform responsePane;

    private Image id;
	
	void Start () {
        this.gameObject.name = "PrayerCard_" + label;

        header = this.gameObject.transform.GetChild(0);
        idBox = header.GetChild(1);
        idPhoto = idBox.GetChild(0);
        profileText = header.GetChild(2);
        message = header.GetChild(3);
        responsePane = this.gameObject.transform.GetChild(1);

        id = idPhoto.GetComponent<Image>();
        id.sprite = character.defaultPhoto;

        profileText.GetComponent<Text>().text = character.name + "\n" +
            character.age + "\n" + character.gender + "\n" + character.bio;

        message.GetComponent<Text>().text = messageText;

        int optionId = 2; //0 - ignore, 1 - smite
        foreach (string response in options) {
            Button responseBtn = Instantiate(responseButton);
            responseBtn.GetComponent<RectTransform>().SetParent(responsePane);
            responseBtn.gameObject.transform.GetChild(0).GetComponent<Text>().text = response;

            ControlLinkedButton cntrl = responseBtn.GetComponent<ControlLinkedButton>();
            cntrl.cardLabel = label;
            cntrl.id = optionId;
            optionId++;
        }

        thisTime = 0;
        elapsedTime = 0;
	}
	
	
	void Update () {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= thisTime) {
            ChangePhoto();
            thisTime = Random.Range(minTimeBetweenFrames, maxTimeBetweenFrames);
            elapsedTime = 0;
        }
	}

    void ChangePhoto() {
        id.sprite = character.animFrames[Random.Range(0, character.animFrames.Length)];
    }

    public void Close() {
        GameObject.Destroy(this.gameObject);
    }
}
