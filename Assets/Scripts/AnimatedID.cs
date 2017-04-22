using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnimatedID : MonoBehaviour {

    public Image image;
    public SpriteRenderer spriteRenderer;
    public float minTimeBetweenFrames;
    public float maxTimeBetweenFrames;

    private float thisTime;
    private float elapsedTime;

    private void Start () {
        image.sprite = spriteRenderer.sprite;
        thisTime = 0;
        elapsedTime = 0;
    }

    void Update () {

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= thisTime) {
            image.sprite = spriteRenderer.sprite;
            thisTime = Random.Range(minTimeBetweenFrames, maxTimeBetweenFrames);
            elapsedTime = 0;
        }

    }
}
