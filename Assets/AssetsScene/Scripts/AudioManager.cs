using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource sfx;
    public AudioClip clickAudio;
    public AudioClip hoverAudio;
    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    public void clickOn()
    {
        sfx.PlayOneShot(clickAudio);
    }

    public void hoverOn()
    {
        sfx.PlayOneShot(hoverAudio);
    }
}
