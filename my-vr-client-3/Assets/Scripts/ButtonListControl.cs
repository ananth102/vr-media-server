using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

public class ButtonListControl : MonoBehaviour
{

    [SerializeField]
    private GameObject button_template;

    string[] data;

    // Start is called before the first frame update
    void Start()
    {
        refresh();
    }

    void refresh(){
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://192.168.68.101:8080/getVideoList"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        if(response == null){
            return;
        }
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        string[] resp = JsonConvert.DeserializeObject<string[]>(jsonResponse);
        data = resp;
        renderElements();
    }

    void renderElements(){
        for(var i = 0; i < data.Length;i++){
            GameObject button = Instantiate(button_template) as GameObject;
            button.SetActive(true);
            button.transform.SetParent(button_template.transform.parent,false);
            var contr = button.GetComponent<ButtonControl>();
            contr.setText(data[i]);
            GameObject child1 = button.transform.GetChild(0).gameObject;
            var tmp = child1.GetComponent<TextMeshProUGUI>();
            var nText = data[i];
            nText = nText.Replace(".mp4","");
            nText = nText.Replace("videos/","");
            
            tmp.text = nText;
        }
    }

}
