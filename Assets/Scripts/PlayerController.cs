using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isInputEnabled = true;
    public AudioSource backgroundMusicAudioSource; // 배경음악을 재생하는 AudioSource
    

    // Update is called once per frame
    void Update()
    {
        if (!isInputEnabled)
        {
            return;
        }
    }
    public void StopBackgroundMusic()
    {
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.Stop();
        }
    }
}
