using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotate : MonoBehaviour {

    public float secondsPerRotation;

    private float elapsedTime;
    private RectTransform rectTf;

    private void Start () {
        rectTf = gameObject.GetComponent<RectTransform>();
        elapsedTime = 0;
    }

    private void Update () {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= secondsPerRotation) {
            elapsedTime = elapsedTime % secondsPerRotation;
        }

        rectTf.rotation = Quaternion.AngleAxis(360 * (elapsedTime / secondsPerRotation), Vector3.forward);
    }


}
