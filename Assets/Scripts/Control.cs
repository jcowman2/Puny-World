﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Control : MonoBehaviour {

    public PrayerCardCreator prayerCardCreator;

    public PrayerNode[] rootNodes;
    public PrayerNode[] queuedNodes;

    private List<PrayerNode> queuedNodeList;

    private List<string> actionList;

	void Start () {
        actionList = new List<string>();
        queuedNodeList = new List<PrayerNode>();

        List<PrayerNode> rootNodeList = new List<PrayerNode>(rootNodes);

        int nodesLength = rootNodeList.Count;
        for (int i = 0; i < nodesLength; i++) {
            int index = Random.Range(0, rootNodeList.Count);
            queuedNodeList.Add(rootNodeList[index]);
            rootNodeList.RemoveAt(index);
        }
        queuedNodes = queuedNodeList.ToArray();
	}
	
	void Update () {
		if (Input.GetKeyDown("space") && queuedNodeList.Count > 0) {
            CreatePrayerCard(queuedNodeList[0]);
            queuedNodeList.RemoveAt(0);
            queuedNodes = queuedNodeList.ToArray();
        }
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

    public void CreatePrayerCard(PrayerNode node) {
        prayerCardCreator.CreateCard(node.label, node.characterName, node.messageText, node.responseOptions);
    }
}
