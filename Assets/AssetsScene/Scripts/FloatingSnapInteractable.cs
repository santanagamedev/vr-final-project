using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingSnapInteractable : SnapInteractable {
    [SerializeField]
    private Transform _floatingTarget; // Intermediate floating position
    [SerializeField]
    private float _floatingSpeed = 2f; // Speed for floating
    [SerializeField]
    private bool _conditionMet = false; // Condition to complete snapping

    private bool _isFloating = false;
    private Transform _floatingObject;

    public void StartFloating(Transform obj) {
        _floatingObject = obj;
        _isFloating = true;
    }

    void Update() {
        if (_isFloating && _floatingObject != null) {
            // Move the object towards the floating target position
            _floatingObject.position = Vector3.Lerp(
                _floatingObject.position,
                _floatingTarget.position,
                Time.deltaTime * _floatingSpeed
            );

            // Check if the condition is met to snap completely
            if (_conditionMet) {
                CompleteSnap();
            }
        }
    }

    private void CompleteSnap() {
        _isFloating = false;

        if (PoseForInteractor(null, out Pose finalPose)) {
            _floatingObject.position = finalPose.position;
            _floatingObject.rotation = finalPose.rotation;

            FloatingSnapInteractor interactor = _floatingObject.GetComponentInChildren<FloatingSnapInteractor>();
            if (interactor != null) {
                interactor.IsFloating = false; // Re-enable interactions
            }

            // Get the DistanceGrabber component (if exists) and disable it
            //_distanceGrabber = _floatingObject.GetComponent<DistanceGrabInteractable>();

            DistanceGrabInteractable distanceGrabInteractable = _floatingObject.GetComponentInChildren<DistanceGrabInteractable>();
            if (distanceGrabInteractable != null) {
                distanceGrabInteractable.enabled = true;
            }


            Debug.Log("Object Snapped Completely!");
        }
    }


    /*
    private void CompleteSnap() {
        _isFloating = false;

        // Snap the object to its final position
        if (PoseForInteractor(null, out Pose finalPose)) {
            _floatingObject.position = finalPose.position;
            _floatingObject.rotation = finalPose.rotation;
            Debug.Log("Object Snapped Completely!");
        }
    }
    */
    public void SetConditionMet(bool condition) {
        _conditionMet = condition;
    }
}
