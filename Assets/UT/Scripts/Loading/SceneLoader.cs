using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this);
        }
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }

    public void LoadScene(string sceneName) {
        if (!Application.CanStreamedLevelBeLoaded(sceneName)) {
            Debug.LogWarning("Scene name is not available");
            return;
        }
        StaticData.sceneToLoad = sceneName;
        SceneManager.LoadScene("Loading");
    }

    public void LoadSceneName(string _name) {
        if (_name!="") {
            SceneManager.LoadScene(_name);
        } else {
            Application.Quit();
            print("keluar");
        }
        
    }
}
