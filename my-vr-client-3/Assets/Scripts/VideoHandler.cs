using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Video;
using UnityEngine.InputSystem;

public class VideoHandler : MonoBehaviour
{

    [SerializeField]
    private VideoPlayer videoPlayer;

    [SerializeField]
    private AudioSource audioSource;

    private bool playState;

    [SerializeField]
    private string videoUrl;
    
    [SerializeField]
    private int videoScale = 3;
    
    [SerializeField]
    private GameObject play_button;
    private int videoHeight;
    private int videoWidth;
    Renderer m_Renderer;
    public Texture play_texture,pause_texture;


    // Start is called before the first frame update
    void Start()
    {
        if(videoPlayer == null){
            Debug.Log("Video player not initalized");
        }
        if(videoUrl != null && videoUrl.Length > 0){
            videoPlayer.url = videoUrl;
            videoPlayer.Prepare();
            Debug.Log(videoPlayer.texture);
            setDimensions();
            play();
            playState = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setVideoUrl(string url){
        videoUrl = url;
        videoPlayer.url = videoUrl;
    }

    public void playDecision(){
        if(playState){
            Debug.Log("Pausing");
            pause();
        }else{
            Debug.Log("Playing");
            play();
        }
    }

    public void pause(){
        bool nullCheck = notnull();
        if(!nullCheck)return;
        videoPlayer.Pause();
        playState = false;
    }

    public void play(){
        bool nullCheck = notnull();
        if(!nullCheck)return;
        videoPlayer.Play();
        playState = true;
    }

    public bool notnull(){
        if(videoPlayer == null){
            Debug.Log("Video player not initalized");
            return false;
        }
        return true;
    }

    public void setDimensions(){
        videoHeight = (int)videoPlayer.texture.height;
        videoWidth = (int)videoPlayer.texture.width;
        float w_t_h_ratio = (float)videoWidth/(float)videoHeight;
        Vector3 updatedScale = new Vector3(w_t_h_ratio*videoScale,videoScale,1);
        gameObject.transform.localScale = updatedScale;
    }
}
