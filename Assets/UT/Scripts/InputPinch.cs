using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputPinch : MonoBehaviour {
    [Range(0, 1)]
    public float sensitivity;
    public bool smooth;
    [Header("Smooth Properties")]
    [Range(0, 10)]
    public float deceleration;
    [HideInInspector]
    public float DeltaPinch { get; private set; }
    private float pinchSpeed;
    private float avgSpeedPinch;
    private bool pinching;

    private event Action TrackInput = delegate { };

    private void Start() {
        pinchSpeed = sensitivity/100;
        if (Application.isMobilePlatform) {
            if (smooth) {
                TrackInput = MobileSmoothInput;
            } else {
                TrackInput = MobileDefaultInput;
            }
        } else {
            if (smooth) {
                TrackInput = EditorSmoothInput;
            } else {
                TrackInput = EditorDefaultInput;
            }
            pinchSpeed *= 300;
        }
    }

    private void Update() {
        TrackInput();
    }

    void MobileInput() {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);
        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
        DeltaPinch = deltaMagnitudeDiff * pinchSpeed;
    }

    void MobileDefaultInput() {
        if (Input.touchCount == 2) {
            MobileInput();
        } else {
            DeltaPinch = 0;
        }
    }

    void MobileSmoothInput() {
        if (Input.touchCount == 2) {
            if (!pinching) {
                avgSpeedPinch = 0;
                pinching = true;
            }
            MobileInput();
            avgSpeedPinch = Mathf.Lerp(avgSpeedPinch, DeltaPinch, Time.deltaTime * deceleration);
        } else {
            if (pinching) {
                DeltaPinch = avgSpeedPinch;
                pinching = false;
            }
            DeltaPinch = Mathf.Lerp(DeltaPinch, 0, Time.deltaTime * deceleration);
        }
    }

    void EditorInput() {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        DeltaPinch = -Input.GetAxis("Mouse ScrollWheel") * pinchSpeed;
    }

    void EditorDefaultInput() {
        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            EditorInput();
        } else {
            DeltaPinch = 0;
        }
    }

    void EditorSmoothInput() {
        if (Input.GetAxis("Mouse ScrollWheel") != 0) {
            if (!pinching) {
                avgSpeedPinch = 0;
                pinching = true;
            }
            EditorInput();
            avgSpeedPinch = Mathf.Lerp(avgSpeedPinch, DeltaPinch, Time.deltaTime * deceleration);
        } else {
            if (pinching) {
                DeltaPinch = avgSpeedPinch;
                pinching = false;
            }
            DeltaPinch = Mathf.Lerp(DeltaPinch, 0, Time.deltaTime * deceleration);
        }
    }
}
