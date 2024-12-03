using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixAudio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void ControlMusica (float sliderMusica)
    {
        audioMixer.SetFloat("VolumenMusica", Mathf.Log10(sliderMusica)*20);
    }

    public void ControlSFX (float sliderMusica)
    {
        audioMixer.SetFloat("VolumenSFX", Mathf.Log10(sliderMusica)*20);
    }
}
