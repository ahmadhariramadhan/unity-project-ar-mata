using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour {
    public Image loadingBar;

    private void Start() {
        SetScreen();
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(StaticData.sceneToLoad);
        while (!asyncLoad.isDone) {
            loadingBar.fillAmount = asyncLoad.progress;
            yield return null;
        }
    }

    private void SetScreen() {
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }
}
