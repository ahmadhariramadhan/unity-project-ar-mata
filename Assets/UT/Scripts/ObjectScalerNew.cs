using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScalerNew : MonoBehaviour {
    [SerializeField]
    private InputPinch smoothPinch;
    public Transform pivot;
    [Tooltip("(min,max) clamp for Scaling")]
    public Vector2 scaleClamp;

    private void Update() {
        if (pivot == null)
            return;
        if (smoothPinch == null)
            return;
        RescaleObject();
    }

    void RescaleObject() {
        pivot.transform.localScale -= new Vector3(smoothPinch.DeltaPinch, smoothPinch.DeltaPinch, smoothPinch.DeltaPinch);
        float scale = Mathf.Clamp(pivot.transform.localScale.x, scaleClamp.x, scaleClamp.y);
        pivot.transform.localScale = new Vector3(scale, scale, scale);
    }
}
