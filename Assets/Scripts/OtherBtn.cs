using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBtn : MonoBehaviour {

    private Control gameControl;

    private void Start () {
        gameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<Control>();
    }


	public void postAction() {
        gameControl.PostAction(gameObject);
    }
}
