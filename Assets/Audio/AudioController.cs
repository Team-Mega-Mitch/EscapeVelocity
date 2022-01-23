using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    int trackNumber = 0;
    private List<int> trackOrder = new List<int>();
    public List<AudioClip> audioTracks = new List<AudioClip>();
    public AudioSource audioSource = new AudioSource();
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
        LoadAudioTracks();
    }
    public void PlayMusic(){
        if(audioSource.isPlaying){
            return;
        }
        audioSource.clip = audioTracks[trackOrder[trackNumber]];
        trackNumber++;
        if(trackNumber == audioTracks.Count){
            trackNumber = 0;
        }
        audioSource.Play();
    }

    public void StopMusic(){
        audioSource.Stop();
    }
    void LoadAudioTracks(){
        for(int i = 0; i < audioTracks.Count; i++){
            int rand;
            do{
                rand = Random.Range(0,audioTracks.Count);
            } while(trackOrder.Contains(rand));
            trackOrder.Add(rand);
        }
    }

    public void SetVolume(float level) {
        audioSource.volume = level;
    }
}
