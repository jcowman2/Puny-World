using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayerNode : MonoBehaviour {

    //There's probably a better way to do this but I suck so there.

    public string label;
    public string characterName;
    public string messageText;
    public string[] responseOptions;
    public Result[] responseResults; //0 - Ignore, 1 - Smite, 2+ - Actual responses

}
