using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingManager : MonoBehaviour {
    [SerializeField] private List<PaintingRotation> paintings;
    [SerializeField] private bool floatingState = false;

    [SerializeField] private Transform ceiling;
    private bool ceilingMoved = false;

    //[SerializeField] private GameObject paintingsParent;

    private void Awake() {
        // Encuentra todas las paintings activas en la escena
        paintings = new List<PaintingRotation>(FindObjectsOfType<PaintingRotation>());
    }

    void Update() {
        if (!ceilingMoved) AllSetFloating(floatingState);
        
        if (floatingState) {
            if (!ceilingMoved) MoveCeilingUp();

            CheckPaintingStates();
        }
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
            floatingState = false;
            AllSetFloating(floatingState);

            // Se abre la puerta que lleva al maestro
        }
    }

    private void AllSetFloating(bool floatingState) {
        foreach (var painting in paintings) {
            painting.SetFloating(floatingState);
        }
    }

    private void MoveCeilingUp() {
        //ceiling.position = new Vector3(ceiling.position.x, ceiling.position.y + 6, ceiling.position.z);
        ceiling.position = new Vector3(ceiling.position.x, 9.96f, ceiling.position.z);

        ceilingMoved = true;
    }
}

