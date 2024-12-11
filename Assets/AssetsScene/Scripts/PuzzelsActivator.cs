using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzelsActivator : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip sfx;
    public AudioClip dialogo;

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
            audioSource.PlayOneShot(sfx);
            Invoke ("DelayDialogo", 1.5f);
        }
    }

    private void DelayDialogo()
    {
        audioSource.PlayOneShot(dialogo);
    }
}
