using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Control : MonoBehaviour {

    public PrayerCardCreator prayerCardCreator;

    public PrayerNode[] rootNodes;
    public PrayerNode[] queuedNodes;
    public PrayerNode[] activeNodes;

    private List<PrayerNode> queuedNodeList;
    private List<PrayerNode> activeNodeList;
    private List<PrayerCard> activePrayerCardList;

    private List<string> actionList;

	void Start () {
        actionList = new List<string>();
        queuedNodeList = new List<PrayerNode>();
        activeNodeList = new List<PrayerNode>();
        activePrayerCardList = new List<PrayerCard>();

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

    public void OnButtonClick (string nodeLabel, int id) {
        Debug.Log("CLICK HEARD:" + nodeLabel + id);

        for (int i = 0; i < activeNodeList.Count; i++) {
            if (activeNodeList[i].label == nodeLabel) {
                Debug.Log("match:" + nodeLabel);
                PrayerNode matchedNode = activeNodeList[i];
                activeNodeList.RemoveAt(i);
                activePrayerCardList[i].Close();
                activePrayerCardList.RemoveAt(i);
                activeNodes = activeNodeList.ToArray();
                //Call results here
                return;
            }
        }
        
    }

    //Depreciated
    public void PostAction (GameObject btn) {

        string label = btn.GetComponentInParent<PrayerCard>().label;
        PrayerNode currentNode;

        Debug.Log("at least made it here: " + activeNodeList.Count);
        foreach (PrayerNode node in activeNodeList) {
            Debug.Log("TEST");
        }

        
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
        activePrayerCardList.Add(prayerCardCreator.CreateCard(node.label, node.characterName, node.messageText, node.responseOptions));
        activeNodeList.Add(node);
        activeNodes = activeNodeList.ToArray();
        Debug.Log("added to array: " + activeNodeList[activeNodeList.Count-1].label);
    }
}
