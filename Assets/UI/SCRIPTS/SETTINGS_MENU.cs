using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SETTINGS_MENU : MonoBehaviour

{
    public AudioMixer audioMixer;
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("MASTER_VOLUME", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("Is Fullscreen");
    }
}
