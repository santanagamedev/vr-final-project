using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettertActivator : MonoBehaviour
{
    Animator animator;
    AudioSource clip;
    private void Start() {
        animator = GetComponent<Animator>();
        clip = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("PlayerOnRange", true);    
            gameObject.GetComponent<Collider>().enabled = false;
            clip.PlayOneShot(clip.clip);
        }
    }
}
