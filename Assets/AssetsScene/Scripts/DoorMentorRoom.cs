using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMentorRoom : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    public AudioSource horrorMusic;

    private void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();  
        horrorMusic = GameObject.Find("MentorRoomActivation").GetComponent<AudioSource>();
    }

    private void OnEnable() {
        PaintingManager.OnPuzzelSolved += HandlePuzzlesolved;
    }

    private void OnDisable() {
        PaintingManager.OnPuzzelSolved -= HandlePuzzlesolved;
    }

    private void HandlePuzzlesolved()
    {
        if (animator != null)
        {
            animator.SetBool("ThirdPuzzleIsSolved", true);
            audioSource.PlayOneShot(audioSource.clip);
            horrorMusic.PlayOneShot(horrorMusic.clip);
        }
        else
        {
            Debug.LogWarning("DoorMentorRoom: Animator no esta asignado");
        }
    }
    

}
