using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour {

    public PlaneManager planeManager; // Reference to SectionManager
    public int fromSectionIndex; // Index of the section to switch to
    public int targetSectionIndex; // Index of the section to switch to

    void OnTriggerEnter(Collider other) {
        Debug.Log($"Triggered by: {other.name} | Tag: {other.tag}");

        if (other.CompareTag("Player")) // Check if the player enters the trigger
        {
            Debug.LogWarning("Collided with Player.");
            planeManager.ChangeToSpecificSection(fromSectionIndex, targetSectionIndex);

            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) // Check if the player enters the trigger
        {
            Debug.LogWarning("Collided with Player on Stay.");
        }
    }
}

