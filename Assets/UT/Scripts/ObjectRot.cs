using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRot : MonoBehaviour {
    [SerializeField]
    private InputSwipe smoothSwipe;
    public Transform pivot;
    public bool inverseRotate;
    public bool lockVertical;
    [Header("Vertical Properties")]
    public bool verticalClamp;
    [Tooltip("(min,max) clamp for Y rotation")]
    public Vector2 clampValue;
    private float direction = 1f;
    private event Action RotateObject = delegate { };

    private void Start() {
        if (inverseRotate)
            direction *= -1;
        RotateObject += RotateObjectDefault;
        if (lockVertical)
            return; 
        RotateObject += RotateObjectWithY;
        if (!verticalClamp)
            return;
        RotateObject += Clamp;
    }

    private void Update() {
        if (pivot == null)
            return;
        if (smoothSwipe == null)
            return;
        RotateObject();
    }

    private void RotateObjectDefault() {
        pivot.transform.localRotation *= Quaternion.Euler(new Vector3(0f, smoothSwipe.DeltaTouch.y * direction, 0));
    }

    private void RotateObjectWithY() {
        pivot.transform.localRotation *= Quaternion.Euler(new Vector3(0, smoothSwipe.DeltaTouch.x * direction,0));
    }

    private void Clamp() {
        float camRotX = pivot.transform.localEulerAngles.x;
        pivot.transform.localEulerAngles = new Vector3(Mathf.Clamp(camRotX >= 180 ?
            camRotX -= 360 : camRotX, clampValue.x, clampValue.y), pivot.transform.localEulerAngles.y, 0);
    }
}
