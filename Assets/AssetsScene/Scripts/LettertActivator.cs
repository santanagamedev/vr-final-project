using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettertActivator : MonoBehaviour
{
    Animator animator;
    AudioSource clip;

    private bool isLetterActive = false;

    [SerializeField] private LayerMask playerLayer; // Layer for the player object
    [SerializeField] private float detectionRadius = 2.0f; // Radius of the detection zone


    private void Start() {
        animator = GetComponent<Animator>();
        clip = GetComponent<AudioSource>();
    }


    /* 
    private void Update() {
        if (!isLetterActive) {
            DetectPlayer();
        }
    }

    private void DetectPlayer() {
        // Check if any player object is within the detection radius
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        foreach (Collider hit in hits) {
            if (hit.CompareTag("Player")) {
                ActivateLetter();
                break; // Exit loop once the player is detected
            }
        }
    }

    private void ActivateLetter() {
        animator.SetBool("PlayerOnRange", true);
        clip.PlayOneShot(clip.clip);
        isLetterActive = true;

        // Disable the trigger collider to prevent further activations
        GetComponent<Collider>().enabled = false;
    }

    private void OnDrawGizmosSelected() {
        // Visualize the detection radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    */

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
