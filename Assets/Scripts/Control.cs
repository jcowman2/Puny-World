using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class Control : MonoBehaviour {

    public PrayerCardCreator prayerCardCreator;

    public GameObject gameOverScreen;
    public GameObject statTextPrefab;
    public Text gameOverResultText;
    public GameObject statPanel;

    public PrayerNode[] rootNodes;
    public PrayerNode[] queuedNodes;
    public PrayerNode[] activeNodes;

    private List<PrayerNode> queuedNodeList;
    private List<PrayerNode> activeNodeList;
    private List<PrayerCard> activePrayerCardList;
    private List<string> newsList;
    private Dictionary<string, double> statDictionary;
    
    private List<string> actionList;

    //public float minTimeActive, maxTimeActive; ADD LATER (multiple prayers at once)
    public float minTimeWaiting, maxTimeWaiting;

    private bool isWaiting = false;
    public float thisTime = 0;
    public float elapsedTime = 0;

    private string DEFAULT_MESSAGE = "Congratulations! You didn't destroy the world!";

    void Start () {

        StartGame();
	}

    public void StartGame() {

        queuedNodeList = new List<PrayerNode>();
        activeNodeList = new List<PrayerNode>();
        activePrayerCardList = new List<PrayerCard>();
        newsList = new List<string>();
        actionList = new List<string>();
        statDictionary = new Dictionary<string, double>();
       
        List<PrayerNode> rootNodeList = new List<PrayerNode>(rootNodes);

        int nodesLength = rootNodeList.Count;
        for (int i = 0; i < nodesLength; i++) {
            int index = UnityEngine.Random.Range(0, rootNodeList.Count);
            queuedNodeList.Add(rootNodeList[index]);
            rootNodeList.RemoveAt(index);
        }
        queuedNodes = queuedNodeList.ToArray();
        
    }

    public void StartCounter() {
        thisTime = UnityEngine.Random.Range(minTimeWaiting, maxTimeWaiting);
        elapsedTime = 0;
        isWaiting = true;
    }
	
	void Update () {
		/*if (Input.GetKeyDown("space") && queuedNodeList.Count > 0) {
            CreatePrayerCard(queuedNodeList[0]);
            queuedNodeList.RemoveAt(0);
            queuedNodes = queuedNodeList.ToArray();
        }*/

        /*else if (Input.GetKeyDown("tab")) {
            AddNodeToQueue(rootNodes[Random.Range(0, rootNodes.Length)], 99);
        }*/

        if (isWaiting) {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= thisTime) {
                isWaiting = false;
                if (queuedNodeList.Count > 0) {
                    CreatePrayerCard(queuedNodeList[0]);
                    queuedNodeList.RemoveAt(0);
                    queuedNodes = queuedNodeList.ToArray();
                } else {
                    DoGameOver(DEFAULT_MESSAGE);
                }
            }
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
                
                HandleResult(matchedNode.responseResults[id]);
                return;
            }
        }
        
    }

    public void HandleResult(Result result) {

        if (result.resultNode != null) {
            AddNodeToQueue(result.resultNode, result.resultNodeDelay);
        }

        for (int n = 0; n < result.additionalNodes.Length; n++) {
            AddNodeToQueue(result.additionalNodes[n], result.additionalNodeDelays[n]);
        }

        if (result.statFormat.Length > 0) {
            UpdateStats(result.statFormat);
        }

        if (result.news.Length > 0) {
            string[] newsSplit = result.news.Split(';');
            
            if (newsSplit.Length > 1) {
                Array.Reverse(newsSplit);
                foreach (string s in newsSplit) {
                    newsList.Insert(0, s);
                }

            } else {
                newsList.Insert(0, newsSplit[0]);
            }

        }

        if (result.gameOver) {
            DoGameOver(result.gameOverMessage);
        } else {
            isWaiting = true;
        }

    }

    public void UpdateStats(string statFormat) {
        string[] stats = statFormat.Split(';');
        string[] splitStat;
        string statName = "";
        double statDouble = 0;

        foreach (string stat in stats) {
            if (stat.Contains("+")) {
                splitStat = stat.Split('+');
                statName = splitStat[0];
                statDouble = double.Parse(splitStat[1]);

                if (statDictionary.ContainsKey(statName)) {
                    statDictionary[statName] = statDictionary[statName] + statDouble;
                } else {
                    statDictionary.Add(statName, statDouble);
                }

            } else if (stat.Contains("-")) {
                splitStat = stat.Split('-');
                statName = splitStat[0];
                statDouble = double.Parse(splitStat[1]);

                if (statDictionary.ContainsKey(statName)) {
                    statDictionary[statName] = statDictionary[statName] - statDouble;
                } else {
                    statDictionary.Add(statName, statDouble);
                }

            } else if (stat.Contains("=")) {
                splitStat = stat.Split('=');
                statName = splitStat[0];
                statDouble = double.Parse(splitStat[1]);

                if (statDictionary.ContainsKey(statName)) {
                    statDictionary[statName] = statDouble;
                } else {
                    statDictionary.Add(statName, statDouble);
                }
            }

            Debug.Log(statName + ":" + statDictionary[statName]);
            thisTime = UnityEngine.Random.Range(minTimeWaiting, maxTimeWaiting);
            elapsedTime = 0;
        }
    }

    public void DoGameOver(string message) {
        Debug.Log("GAME OVER:" + message);
        //GameObject.FindWithTag("EndGameText").GetComponent<Text>().text = message; //yum

        gameOverResultText.text = message;

        foreach (Transform child in statPanel.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach (string key in statDictionary.Keys) {
            GameObject temp = Instantiate(statTextPrefab);
            temp.GetComponent<Text>().text = key + ": " + statDictionary[key];
            temp.transform.SetParent(statPanel.transform);
        }

        gameOverScreen.gameObject.SetActive(true);
    }

    public void AddNodeToQueue(PrayerNode node, int delay) {
        if (delay > queuedNodeList.Count) {
            delay = queuedNodeList.Count;
        }

        queuedNodeList.Insert(delay, node);
        queuedNodes = queuedNodeList.ToArray();
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
        //Debug.Log("added to array: " + activeNodeList[activeNodeList.Count-1].label);
    }
}
