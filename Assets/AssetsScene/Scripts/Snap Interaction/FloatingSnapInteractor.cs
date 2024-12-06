using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script de los LIBROS

public class FloatingSnapInteractor : SnapInteractor {
    private DistanceGrabInteractable _distanceGrabInteractable; // Store the DistanceGrabInteraction component reference
    private DistanceHandGrabInteractable _distanceHandGrabInteractable; 
    private FloatingSnapInteractable _currentInteractable;

    [Header("Customs")]
    public InteractionTags interactorTag;
    public bool IsValidPair(InteractionTags interactableTag) {
        return interactorTag == interactableTag;  // Checks if both tags match
    }

    private bool _isFloating = false; // Add a flag to track floating state
    public bool IsFloating {
        get => _isFloating;
        set => _isFloating = value;
    }

    protected override void InteractableSelected(SnapInteractable interactable) {
        base.InteractableSelected(interactable);

        if (interactable is FloatingSnapInteractable floatingInteractable) {
            _currentInteractable = floatingInteractable; // Guarda referencia al interactable
            floatingInteractable.StartInteraction(this); // Registra la interacción
            //Debug.Log($"{name} comenzó a interactuar con {floatingInteractable.name}");

            // Get the Grabbable component, if present, and disable it
            //_distanceGrabInteractable = GetComponent<DistanceGrabInteractable>();
            GetGrabbables();

            // Disable grabbing temporarily
            /*
            if (_distanceGrabInteractable != null) {
                _distanceGrabInteractable.enabled = false; 
            } */
            DisableGrabbing();

            Rigidbody rb = floatingInteractable.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            // Set the flag to disable the interactor (indirectly)
            _isFloating = true;

            floatingInteractable.StartFloating(this.transform.parent);
        }
    }

    private void Update() {
        if (_isFloating) {
            // Skip interaction logic during floating
            return;
        }

        // Continue normal interactor behavior
        base.Drive();
    }

    private void GetGrabbables() {
        _distanceGrabInteractable = GetComponent<DistanceGrabInteractable>();
        _distanceHandGrabInteractable = GetComponent<DistanceHandGrabInteractable>();
    }

    public void DisableGrabbing() {
        GetGrabbables();

        if (_distanceGrabInteractable != null) {
            _distanceGrabInteractable.enabled = false;
        }
        if (_distanceHandGrabInteractable != null) {
            _distanceHandGrabInteractable.enabled = false;
        }
    }

    // Optionally, re-enable the Grabbable component if necessary
    public void EnableGrabbing() {
        if (_distanceGrabInteractable != null) {
            _distanceGrabInteractable.enabled = true;
        }
        if (_distanceHandGrabInteractable != null) {
            _distanceHandGrabInteractable.enabled = true;
        }
    }


    public bool HasValidInteraction() {
        return _currentInteractable != null; // Devuelve true si está emparejado
    }
    public FloatingSnapInteractable GetCurrentInteractable() {
        return _currentInteractable;
    }

    // Tal vez este ya no sea necesario
    protected override void InteractableUnselected(SnapInteractable interactable) {
        base.InteractableUnselected(interactable);
        _currentInteractable = null; // Limpia la referencia
        Debug.Log($"{name} dejó de interactuar.");
    }

    public void CallInteractableUnselected(SnapInteractable interactable) {
        InteractableUnselected(interactable);
    }

    public void PushBook(float pushForce) {
        //Rigidbody rb = interactor.GetComponentInParent<Rigidbody>();
        Rigidbody rb = transform.GetComponent<Rigidbody>();

        if (rb != null) {
            Debug.LogWarning("Book pushed.");
            rb.AddForce(Vector3.left * pushForce, ForceMode.Impulse);
        } else {
            Debug.LogWarning("Book Rigidbody NOT FOUND .");
        }
        
    }
}

