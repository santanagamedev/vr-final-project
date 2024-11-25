using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour {

    public GameObject[] sections; // Array to hold sections of the map

    void Start() {
        //ShowSection(currentSectionIndex); // Display the first section
    }

    public void ChangeToSpecificSection(int fromSectionIndex, int toSectionIndex) {
        // Ensure the provided index is valid
        if (fromSectionIndex < 0 || fromSectionIndex >= sections.Length) {
            Debug.LogWarning("Invalid section index: " + fromSectionIndex);
            return;
        }
        if (toSectionIndex < 0 || toSectionIndex >= sections.Length) {
            Debug.LogWarning("Invalid section index: " + toSectionIndex);
            return;
        }

        sections[toSectionIndex].transform.position = sections[fromSectionIndex].transform.position;

        HideSection(fromSectionIndex);

        ShowSection(toSectionIndex);
    }


    private void ShowSection(int index) {
        sections[index].SetActive(true);
    }

    private void HideSection(int index) {
        sections[index].SetActive(false);
    }
}


