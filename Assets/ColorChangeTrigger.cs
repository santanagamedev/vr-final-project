using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour {
    //public PlaneManager planeManager; // Reference to SectionManager
    public int fromSectionIndex; // Index of the section to switch to
    public int targetSectionIndex; // Index of the section to switch to

    public GameObject planeToChange;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) // Check if the player enters the trigger
        {
            //Debug.LogWarning("Collided with Player.");
            //planeManager.ChangeToSpecificSection(fromSectionIndex, targetSectionIndex);

            planeToChange.GetComponent<Renderer>().material.color = Color.red; 

            //Destroy(gameObject);
        }
    }
}
