using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InformationHandler : MonoBehaviour
{
    [SerializeField]
    private Text titleText;
    [SerializeField]
    private Text infoText;

    public List<Information> listInfo = new List<Information>();
    [Serializable]
    public class Information {
        public string title;
        [TextArea]
        public string description;
    }
    private void Start() {
        this.gameObject.SetActive(false);
    }

    public void Close() {
        SoundFX.Instance.PlaySound(0);
        this.gameObject.SetActive(false);
    }

    public void OpenInformation(int index) {
        SoundFX.Instance.PlaySound(0);
        this.gameObject.SetActive(true);
        titleText.text = listInfo[index].title;
        infoText.text = listInfo[index].description;
    }
}
