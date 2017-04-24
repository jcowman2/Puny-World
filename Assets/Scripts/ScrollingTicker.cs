using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTicker : MonoBehaviour {

    public float speed;

    private RectTransform rectTransform;

    void Start () {
        rectTransform = gameObject.GetComponent<RectTransform>();
    }
	
	
	void Update () {
        rectTransform.position = new Vector3(rectTransform.position.x - speed * Time.deltaTime,
            rectTransform.position.y, rectTransform.position.z);
	}
}
