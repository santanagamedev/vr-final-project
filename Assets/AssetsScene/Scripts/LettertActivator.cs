using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettertActivator : MonoBehaviour {
    Animator animator;
    AudioSource clip;

    private bool isLetterActive = false;

    [SerializeField] private LayerMask playerLayer; // Layer for the player object
    [SerializeField] private float detectionRadius = 2.0f; // Radius of the detection zone


    private void Start() {
        animator = GetComponent<Animator>();
        clip = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (!isLetterActive) {
            if (other.gameObject.CompareTag("Player")) {
                animator.SetBool("PlayerOnRange", true);
                gameObject.GetComponent<Collider>().enabled = false;
                clip.PlayOneShot(clip.clip);

                isLetterActive = true;
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (!isLetterActive) {
            if (other.gameObject.CompareTag("Player")) {
                animator.SetBool("PlayerOnRange", true);
                gameObject.GetComponent<Collider>().enabled = false;
                clip.PlayOneShot(clip.clip);

                isLetterActive = true;
            }
        }
    }
}
