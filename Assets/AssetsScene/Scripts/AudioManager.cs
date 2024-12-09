using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private AudioSource sfx;
    public AudioClip clickAudio;
    public AudioClip hoverAudio;
    public AudioClip dialogoPuzzle1;
    public AudioClip dialogoPuzzle2;
    [SerializeField] private AudioMixer audioMixer;
    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    //Reproduciccion de Dialogos
    public void PlayDialogo1()
    {
        sfx.PlayOneShot(dialogoPuzzle1, 0.06f);
    }

    public void PlayDialogo2()
    {
        sfx.PlayOneShot(dialogoPuzzle2, 0.06f);
    }

    //Sonidos de estado de botones
    public void clickOn()
    {
        sfx.PlayOneShot(clickAudio);
    }

    public void hoverOn()
    {
        sfx.PlayOneShot(hoverAudio);
    }

    //Control de Audio
    public void ControlMusica (float sliderMusica)
    {
        audioMixer.SetFloat("VolumenMusica", Mathf.Log10(sliderMusica)*20);
    }

    public void ControlSFX (float sliderMusica)
    {
        audioMixer.SetFloat("VolumenSFX", Mathf.Log10(sliderMusica)*20);
    }
}
