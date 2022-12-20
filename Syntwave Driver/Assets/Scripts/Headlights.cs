using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headlights : MonoBehaviour
{

    [SerializeField] bool closing;
    [SerializeField] bool opening;

    [SerializeField] bool opened;
    [SerializeField] bool closed;
    
    private float openingDuration;
    [SerializeField] private float currentOpening;

    [SerializeField] Quaternion openedState;
    [SerializeField] Quaternion closedState;

    [SerializeField] Vector3 currrentAngle;

    void Start()
    {
        closing = false;
        opening = false;

        closed = true;
        opened = false;

        openedState = Quaternion.Euler(-45, 0, 0);
        closedState = Quaternion.Euler(0, 0, 0);

        openingDuration = 1f;
        currentOpening = 0f;

        transform.Rotate(0, 0, 0);

    }
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        changeState();
        currrentAngle = transform.localEulerAngles;
    }
    void HandleInput() {
        if (!Input.GetKeyDown(KeyCode.N)) return;
        if (closed) {
            opening = true;
            closing = false;
            return;
        }
        if (opened) {
            closing = true;
            opening = false;
        }
    }
    void changeState() {
        if (closing) {
            if (currentOpening < openingDuration) {
                transform.Rotate(Mathf.Lerp(0, 2, currentOpening * openingDuration), 0, 0, Space.Self);
                currentOpening += Time.deltaTime;
                opened = false;
            } else {
                currentOpening = 0;
                closed = true;
                closing = false;
            }
        }
        if (opening) {
            if (currentOpening < openingDuration) {
                transform.Rotate(Mathf.Lerp(0, -2, currentOpening * openingDuration), 0, 0, Space.Self);
                currentOpening += Time.deltaTime;
                closed = false;
            } else {
                currentOpening = 0;
                opened = true;
                opening = false;
            }
        }
    }
}
