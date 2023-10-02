using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class VolumeMenuHook : MonoBehaviour
{
    public AudioMixer mixer;

    float volume = 0f;

    public void OnValueChange(float newValue)
    {
        volume = (80f * newValue) - 80f;
        Debug.Log(newValue);
    }

    public void Update()
    {
        mixer.SetFloat("MasterVol", volume);
    }
}
