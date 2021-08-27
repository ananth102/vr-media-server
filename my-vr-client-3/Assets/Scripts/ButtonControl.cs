using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    [SerializeField]
    public GameObject mytextObj;
    
    [SerializeField]
    private GameObject videoScreen;

    string currentURL = "";
    
    public void setText(string text){
        currentURL = "http://192.168.68.101:8080/" + text;
    }
    void Start(){
        gameObject.GetComponent<Button> ().onClick.AddListener(delegate { oc(); });
    }
    public void oc(){
        // Debug.Log("wowowowoowowowowowoowgghh44444ow");
        var vp = videoScreen.GetComponent<VideoHandler>();
        vp.setVideoUrl(currentURL);
    }
}
