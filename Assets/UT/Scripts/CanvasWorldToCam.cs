using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasWorldToCam : MonoBehaviour
{
    private Transform cam;

    private void Start() {
        cam = Camera.main.transform;
    }

    private void Update() {
        this.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x,
            cam.transform.eulerAngles.y, 0);
    }
}
