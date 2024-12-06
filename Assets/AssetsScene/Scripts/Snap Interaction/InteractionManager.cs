using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEngine.Rendering.DynamicArray<T>;

public class InteractionManager : MonoBehaviour {
    [SerializeField] private FloatingSnapInteractor[] interactors;
    [SerializeField] private FloatingSnapInteractable[] interactables;

    private bool isPuzzleActivated = false;
    [SerializeField] private float pushForce = 5.0f;

    [SerializeField] private bool generalPuzzleActivator = false;

    private void Awake() {
        // Encuentra todos los interactores e interactables dinámicamente
        interactors = FindObjectsOfType<FloatingSnapInteractor>();
        interactables = FindObjectsOfType<FloatingSnapInteractable>();
    }

    private void Start() {
        // Deactivate all DistanceGrabbables and DistanceHandGrabbables
        DeactivateInteractors();
        StopAllParticles();

        //ActivateInteractables();
    }

    private void DeactivateInteractors() {
        foreach (var interactor in interactors) {
            interactor.DisableGrabbing();
            Debug.LogWarning("Interactor grabbing Disabled. -------------");
        }
    }

    private void StopAllParticles() {
        foreach(var interactable in interactables) { 
            interactable.StopParticles();
        }
    }
    private void PlayAllParticles() {
        foreach(var interactable in interactables) { 
            interactable.PlayParticles();
        }
    }

    private void Update() {
        if (AllPaired()) {
            Debug.Log("¡Todos los interactors están emparejados con interactables!");

            CheckAllPairings();
        }

        if (generalPuzzleActivator) {
            ActivatePuzzle();
        }
    }

    private bool AllPaired() {
        // Verifica que cada interactor tenga un interactable y viceversa
        foreach (var interactor in interactors) {
            if (!interactor.HasValidInteraction()) {
                return false; // Un interactor no está emparejado
            }
        }

        foreach (var interactable in interactables) {
            if (!interactable.HasInteractor()) {
                return false; // Un interactable no está emparejado
            }
        }

        return true; // Todos están emparejados
    }

    private void CheckAllPairings() {
        bool allPairsMatch = true;

        foreach (var interactor in interactors) {
            //Debug.Log($"{interactor.name} has tag: {interactor.interactorTag}");
            //Debug.Log($"Interactor tag: {Enum.GetName(typeof(InteractionTags), interactor.interactorTag)}");
            Debug.Log($"Interactor {interactor.name} tag value: {(int)interactor.interactorTag}");

            var currentInteractable = interactor.GetCurrentInteractable(); // Get the paired interactable
            Debug.Log($"Interactor {interactor.name} current Interactable is: {currentInteractable}");
            //if (currentInteractable == null || !interactor.IsInteractingWith(currentInteractable) || !currentInteractable.IsInteractedBy(interactor)) {
            if (currentInteractable == null || !(interactor.interactorTag == currentInteractable.interactableTag)) {
                Debug.LogWarning($"{interactor.name} is NOT interacting correctly.");
                allPairsMatch = false;
                break;
            }

            Debug.Log($"{interactor.name} is interacting correctly with {currentInteractable.name}.");
        }

        if (allPairsMatch) {
            Debug.Log("All pairs match. Trigger success behavior.");
            foreach (var interactable in interactables) { 
                interactable.SetConditionMet(true);
            }

        } else {
            Debug.LogWarning("Not all pairs match. Trigger failure behavior.");
            foreach (var interactable in interactables) {
                interactable.BreakSnap();
                //interactable.SetPairedFailed(true); 
            }
        }

        /*
        foreach (var interactor in interactors) {
            int interactorCounter = 0;
            foreach (var interactable in interactables) {

                if (!interactor.IsValidPair(interactable.interactableTag)) {
                    Debug.LogWarning($"{interactor.name} is NOT interacting with {interactable.name} correctly.");
                } else {
                    Debug.Log($"{interactor.name} is interacting correctly with {interactable.name}.");
                    interactorCounter++;

                    interactable.SetConditionMet(true);
                }

                /*
                if (interactor.IsValidPair(interactable.interactableTag)) {
                    Debug.Log($"{interactor.name} is interacting correctly with {interactable.name}.");
                    interactable.SetConditionMet(true);
                } else {
                    Debug.LogWarning($"{interactor.name} is NOT interacting with {interactable.name} correctly.");
                }
                * /
            }
        }
        */

    }

    private void ActivatePuzzle() {
        //isPuzzleActivated = true;

        PushBooks();
        ActivateInteractors();
        PlayAllParticles();
        //ActivateInteractables();

        generalPuzzleActivator = false;
    }

    private void ActivateInteractors() {
        foreach (var interactor in interactors) {
            interactor.EnableGrabbing();
            Debug.LogWarning("Interactor grabbing Enabled. -------------");
        }
    }

    private void PushBooks() {
        foreach (var interactor in interactors) {
            Rigidbody rb = interactor.GetComponentInParent<Rigidbody>();
            if (rb != null) {
                //Debug.LogWarning("Books pushed.");
                rb.AddForce(Vector3.left * pushForce, ForceMode.Impulse);
            }
        }
    }

}
