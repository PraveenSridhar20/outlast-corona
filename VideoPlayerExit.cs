using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerExit : MonoBehaviour
{
    // Start is called before the first frame update
    VideoPlayer videoPlayer;
    void Start()
    {
        videoPlayer=GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!videoPlayer.isPlaying&&videoPlayer.isPrepared){
            print("time to quit");
            Application.Quit();
        }
    }
}
