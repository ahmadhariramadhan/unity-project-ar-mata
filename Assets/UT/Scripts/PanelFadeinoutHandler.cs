using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFadeinoutHandler : MonoBehaviour
{
    [SerializeField]
    private Text tittleText;
    [SerializeField]
    [TextArea]
    private string[] contensTransition;
    private void Start() {
        StartCoroutine(InitPanelFadeinFadeout());
    }
    public IEnumerator InitPanelFadeinFadeout() {
        
        startTransition(0);
        yield return new WaitForSeconds(2f);
        startTransition(1);
    }

    private void startTransition(int _index) {
        StartCoroutine(FadeIn(0f, tittleText.color.a));
        tittleText.text = contensTransition[_index];
    }

    private IEnumerator FadeIn(float _startValue,float _endValue) {
        float duration = 1f; //0.5 secs
        float currentTime = 0f;
        while (currentTime < duration) {
            float alpha = Mathf.Lerp(_startValue, _endValue, currentTime / duration);
            tittleText.color = new Color(tittleText.color.r, tittleText.color.g, tittleText.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
