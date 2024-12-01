using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script de los EMPTY SPACES

public class FloatingSnapInteractable : SnapInteractable {
    [SerializeField]
    private Transform _floatingTarget; // Intermediate floating position
    [SerializeField]
    private float _floatingSpeed = 2f; // Speed for floating
    [SerializeField]
    private bool _conditionMet = false; // Condition to complete snapping
    [SerializeField]
    private bool _failedPairing = false; // Condition to complete snapping

    [SerializeField] private float floatingAmplitude = 0.08f;
    [SerializeField] private float resettingDelayTime = 2.0f;

    [SerializeField] private bool _isFloating = false;
    private Transform _floatingObject;

    private float randomSpeed;
    private bool enableSnappingAfterSnapCompleted = false;

    private FloatingSnapInteractor _currentInteractor;
    public InteractionTags interactableTag;

    public void StartInteraction(FloatingSnapInteractor interactor) {
        _currentInteractor = interactor;
        //Debug.Log($"{name} est� siendo manipulado por {interactor.name}");
    }

    public void StartFloating(Transform obj) {
        _floatingObject = obj;  // Aqu� pasamos el cubo como tal
        _isFloating = true;

        randomSpeed = Random.Range(0.0f, 0.4f);
    }

    public bool HasInteractor() {
        return _currentInteractor != null; // Devuelve true si tiene un interactor
    }

    void Update() {
        if (_isFloating && _floatingObject != null) {
            // Calculate sinuous offset for up-down motion
            float sinOffset = Mathf.Sin(Time.time * (_floatingSpeed + randomSpeed)) * floatingAmplitude; // Amplitude of 0.5 units

            // Create a new target position with oscillating Y-axis
            Vector3 oscillatingTarget = new Vector3(
                _floatingTarget.position.x,
                _floatingTarget.position.y + sinOffset, // Add oscillation to Y-axis
                _floatingTarget.position.z
            );

            // Move the object towards the oscillating target
            _floatingObject.position = Vector3.Lerp(
                _floatingObject.position,
                oscillatingTarget,
                Time.deltaTime * _floatingSpeed
            );

            // Check if the condition is met to snap completely
            if (_conditionMet) {
                CompleteSnap();
            }
            
            // Check if at least one pair is coupled incorrectly
            if (_failedPairing) {
                BreakSnap();
            }
        }
    }

    private void CompleteSnap() {
        Debug.Log("Entered CompleteSnap()");
        _isFloating = false;

        if (PoseForInteractor(null, out Pose finalPose)) {
            GameObject placeholder = transform.GetChild(0).gameObject;
            placeholder.SetActive(false);

            _floatingObject.rotation = finalPose.rotation;
            //_floatingObject.position = finalPose.position;
            _floatingObject.position = Vector3.Lerp(
                _floatingObject.position,
                finalPose.position,
                Time.deltaTime * _floatingSpeed
            );

            FloatingSnapInteractor interactor = _floatingObject.GetComponentInChildren<FloatingSnapInteractor>();
            if (interactor != null) {
                interactor.IsFloating = false; // Re-enable interactions
                if(enableSnappingAfterSnapCompleted) {
                    interactor.EnableGrabbing();
                    EnableSnapping();
                } else {
                    DisableSnapping();
                }
            }

            Debug.Log($"Object {name} Snapped Completely!");
        }
    }

    // This would be the opposite of StartFloating()
    private void BreakSnap() {
        Debug.Log("Entered BreakSnap()");
        _isFloating = false;

        FloatingSnapInteractor interactor = _floatingObject.GetComponentInChildren<FloatingSnapInteractor>();
        interactor.CallInteractableUnselected(this);

        Rigidbody rb = _floatingObject.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        if (interactor != null) {
            interactor.IsFloating = false; // Re-enable interactions
            interactor.EnableGrabbing();
        }

        GameObject placeholder = transform.GetChild(0).gameObject;
        placeholder.SetActive(true);

        StopInteraction();
        _floatingObject = null;

        _failedPairing = false;
    }

    // Tal vez este ya no sea necesario
    public void StopInteraction() {
        Debug.Log($"{name} ya no est� siendo manipulado.");
        _currentInteractor = null;
    }

    // Este deber�a llamarse desde el Interaction Manager
    public void SetConditionMet(bool condition) {
        _conditionMet = condition;
    }
    // Este deber�a llamarse desde el Interaction Manager
    public void SetPairedFailed(bool condition) {
        _failedPairing = condition;
    }

    /*
    protected override void InteractorAdded(SnapInteractor interactor) {
        base.InteractorAdded(interactor);
        Debug.Log($"Interactor Added: {interactor.name}");
    }

    protected override void InteractorRemoved(SnapInteractor interactor) {
        base.InteractorRemoved(interactor);
        Debug.Log($"Interactor Removed: {interactor.name}");
    }
    */

    protected override void InteractorAdded(SnapInteractor interactor) {
        // Only proceed with base interaction logic if snapping is allowed
        if (enableSnappingAfterSnapCompleted) {
            base.InteractorAdded(interactor);
        }
    }

    public void DisableSnapping() {
        enableSnappingAfterSnapCompleted = false;
    }

    public void EnableSnapping() {
        enableSnappingAfterSnapCompleted = true;
    }
}
