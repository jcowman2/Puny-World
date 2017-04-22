using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
    
    public Transform target;
    private bool mouseDown = false;
    private Vector3 mouseStartPos;
    private Vector3 startPos;
    public bool doReturn;

    public bool bounded;
    public float xMin, xMax;
    public float yMin, yMax;

    public void OnPointerDown(PointerEventData data) {
        mouseDown = true;
        startPos = target.position;
        mouseStartPos = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData data) {
        mouseDown = false;
        if (doReturn) target.position = startPos;
    }

    private void Update () {
        if (mouseDown) {
            Vector3 currentPos = Input.mousePosition;
            Vector3 deltaPos = currentPos - mouseStartPos;
            Vector3 newPos = startPos + deltaPos;

            if (bounded) {

                if (newPos.x < xMin) {
                    newPos.x = xMin;
                } else if (newPos.x > xMax) {
                    newPos.x = xMax;
                }

                if (newPos.y < yMin) {
                    newPos.y = yMin;
                } else if (newPos.y > yMax) {
                    newPos.y = yMax;
                }

            }

            target.position = newPos;
        }
    }
}
