using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBtn : MonoBehaviour {

    public int clickId;

    private Control gameControl;
    private string cardLabel;

    private void Start () {
        gameControl = GameObject.FindGameObjectWithTag("GameControl").GetComponent<Control>();
        cardLabel = gameObject.GetComponentsInParent<PrayerCard>()[0].label;
    }

    public void PostClick() {
        gameControl.OnButtonClick(cardLabel, clickId);
    }


	public void postAction() { //depreciated
        gameControl.PostAction(gameObject);
    }
}
