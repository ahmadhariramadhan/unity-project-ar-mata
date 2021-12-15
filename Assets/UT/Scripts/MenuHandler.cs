using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    private void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }
    private void Update() {
        Back();
    }
    private void Back() {
        if (Input.GetKey(KeyCode.Escape)) {
            SceneLoader.Instance.LoadSceneName(sceneName);
        }
    }
}
