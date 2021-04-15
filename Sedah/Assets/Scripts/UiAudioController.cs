using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiAudioController : MonoBehaviour
{    
    public TextMeshProUGUI textMesh;
    public float startSize;
    public float sizeChangeParam;
    
    private void Awake() //I guess I should put most my refrence grabbing on awake not start
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        startSize = textMesh.fontSize;
    }

    private void Update() {
        ListenToVolumeChange();
    }
    public void ListenToVolumeChange()
    {
        if (!gameObject.activeSelf)
            return;
        textMesh.fontSize = Mathf.Lerp(textMesh.fontSize, startSize * (AudioVisualizeManager.Output_Volume + 1), Time.deltaTime * sizeChangeParam);
    }
}
