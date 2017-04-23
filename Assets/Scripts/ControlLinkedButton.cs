using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLinkedButton : MonoBehaviour {

    public string cardLabel;
    public int id;

    private Control control;

	
	void Start () {
        control = GameObject.FindWithTag("GameControl").GetComponent<Control>();
	}

    public void PostClick() {
        control.OnButtonClick(cardLabel, id);
    }
	
	
	void Update () {
		
	}
}
