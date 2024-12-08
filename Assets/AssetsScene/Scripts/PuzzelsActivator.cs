using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzelsActivator : MonoBehaviour
{
    AudioSource audioSource;
    private InteractionManager puzzelBooks;
    public bool puzzelBooksIsEnable = false;

    private void Start() {
        puzzelBooks = FindObjectOfType<InteractionManager>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player") && !puzzelBooksIsEnable  )
        {
            puzzelBooksIsEnable = true;
            puzzelBooks.generalPuzzleActivator = true;
            audioSource.PlayOneShot(audioSource.clip);
            
        }
    }
}
