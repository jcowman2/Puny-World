using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsreelSpawner : MonoBehaviour {

    public GameObject tickerPrefab;

    private GameObject currentTicker;

    public void ChangeTicker(string message) {
        if (currentTicker != null) {
            GameObject.Destroy(currentTicker);
        }

        currentTicker = GameObject.Instantiate(tickerPrefab);
        currentTicker.transform.parent = transform;
        currentTicker.transform.position = transform.position;
        currentTicker.GetComponent<Text>().text = message;
    }

	public void ClearTicker() {
        if (currentTicker != null) {
            GameObject.Destroy(currentTicker);
        }
    }
}
