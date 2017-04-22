using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerCardCreator : MonoBehaviour {

    public bool createCard;

    public GameObject templateCard;

    void CreateCard() {
        Debug.Log("Creating Card");
    }
	
	void Start () {
		
	}
	
	
	void Update () {
		
        if (createCard) {
            CreateCard();
            createCard = false;
        }

	}
}
