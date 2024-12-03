using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour {
    [SerializeField] private List<PaintingRotation> paintings;

    private void Awake() {
        // Encuentra todas las paintings activas en la escena
        paintings = new List<PaintingRotation>(FindObjectsOfType<PaintingRotation>());
    }

    void Update() {
        CheckPaintingStates();
    }

    private void CheckPaintingStates() {
        List<PaintingRotation> incorrectlyOriented = new List<PaintingRotation>();

        foreach (var painting in paintings) {
            bool shouldFaceWall = painting.GetShouldFaceWall();
            bool isFacingWall = painting.CheckIfFacingWall();

            if (shouldFaceWall != isFacingWall) {
                incorrectlyOriented.Add(painting);
            }
        }

        // Process the incorrectly oriented paintings
        foreach (var painting in incorrectlyOriented) {
            Debug.LogWarning($"{painting.name} is incorrectly oriented!");
        }

        if (incorrectlyOriented.Count == 0) {
            Debug.Log("All paintings are correctly oriented.");
        }
    }
}

