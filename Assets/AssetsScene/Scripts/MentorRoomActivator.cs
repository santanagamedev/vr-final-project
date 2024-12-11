using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MentorRoomActivator : MonoBehaviour
{
    //AudioSource audioSource;

    private void Start() {
        //audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame() 
    {
        //audioSource.PlayOneShot(audioSource.clip);
        yield return new WaitForSeconds(12);
        SceneManager.LoadScene(0);
    }

    
}
