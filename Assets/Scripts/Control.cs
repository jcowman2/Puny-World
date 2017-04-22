using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Control : MonoBehaviour {

    public List<string> actionList;

	void Start () {
        actionList = new List<string>();
	}
	
	void Update () {
		
	}

    public void PostAction (GameObject btn) {

        string label = btn.GetComponentInParent<PrayerCard>().label;
        string outputCode = label + "_";

        if (btn.CompareTag("Response")) {
            outputCode += btn.GetComponentInChildren<Text>().text.Replace(" ", "");
        } else if (btn.CompareTag("Random")) {
            outputCode += "Random";
        } else if (btn.CompareTag("Ignore")) {
            outputCode += "Ignore";
        } else if (btn.CompareTag("Smite")) {
            outputCode += "Smite";
        }

        Debug.Log(outputCode);
        actionList.Add(outputCode);

    }
}
