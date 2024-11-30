using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSnapInteractor : SnapInteractor {
    private DistanceGrabInteractable _distanceGrabInteractable; // Store the DistanceGrabInteraction component reference

    private bool _isFloating = false; // Add a flag to track floating state
    public bool IsFloating {
        get => _isFloating;
        set => _isFloating = value;
    }

    protected override void InteractableSelected(SnapInteractable interactable) {
        base.InteractableSelected(interactable);

        if (interactable is FloatingSnapInteractable floatingInteractable) {
            if (floatingInteractable == null) {
                Debug.LogWarning("FloatingSnapInteractable is null.");
                return;
            } else {
                Debug.LogWarning("FloatingSnapInteractable is NOT NULL.");
            }

            // Get the Grabbable component, if present, and disable it
            _distanceGrabInteractable = floatingInteractable.GetComponent<DistanceGrabInteractable>();
            if (_distanceGrabInteractable != null) {
                Debug.LogWarning("distanceGrabInteraction FOUND.");
                _distanceGrabInteractable.enabled = false; // Disable grabbing temporarily
            } else {
                Debug.LogWarning("distanceGrabInteraction NOT found.");
            }

            Rigidbody rb = floatingInteractable.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            // Set the flag instead of disabling the interactor
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


    /*
    protected override void InteractableSelected(SnapInteractable interactable) {
        base.InteractableSelected(interactable);

        if (interactable is FloatingSnapInteractable floatingInteractable) {
            Rigidbody rb = floatingInteractable.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.useGravity = false;
                rb.isKinematic = true;
            }

            // Temporarily disable the interactor to prevent snap conflicts
            this.enabled = false;

            //floatingInteractable.StartFloating(this.transform);
            floatingInteractable.StartFloating(this.transform.parent);
        }
    }
    */

    // Optionally, re-enable the Grabbable component if necessary
    public void EnableGrabbing() {
        if (_distanceGrabInteractable != null) {
            _distanceGrabInteractable.enabled = true;
        }
    }
}

