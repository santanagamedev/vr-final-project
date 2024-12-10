using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingRotation : MonoBehaviour {
    [SerializeField] private float initialRotation;

    [SerializeField] private bool isFacingWall = false;
    [SerializeField] private bool shouldFaceWall = false;

    private float _floatingSpeed = 1.3f; // Speed for floating
    private float floatingAmplitude = 0.0015f;
    private float randomSpeed;

    private bool isFloating = false;

    private const float minRelativeAngle = 150f; // Min relative angle from initial rotation
    private const float maxRelativeAngle = -150f; // Max relative angle from initial rotation

    void Start() {
        randomSpeed = Random.Range(0.0f, 0.4f);
        initialRotation = transform.rotation.eulerAngles.y;
    }

    void Update() {
        if (isFloating) {
            CheckIfFacingWall();

            // Calculate sinuous offset for up-down motion
            float sinOffset = Mathf.Sin(Time.time * (_floatingSpeed + randomSpeed)) * floatingAmplitude;

            transform.position = new Vector3(transform.position.x,
                                             transform.position.y + sinOffset,
                                             transform.position.z);
        }
    }

    public bool CheckIfFacingWall() {
        float currentYRotation = transform.rotation.eulerAngles.y;
        float relativeAngle = Mathf.DeltaAngle(initialRotation, currentYRotation); // Calculate the relative angle

        Debug.Log($"{name}: Relative Angle = {relativeAngle}");

        // Check if the relative angle is within the range
        //if (relativeAngle >= minRelativeAngle && relativeAngle <= maxRelativeAngle) {
        if (Mathf.Abs(relativeAngle) >= minRelativeAngle) {
            isFacingWall = true;
        } else {
            isFacingWall = false;
        }

        return isFacingWall;
    }

    public bool GetShouldFaceWall() {
        return shouldFaceWall;
    }

    public void SetFloating(bool floatingState) {
        isFloating = floatingState;
    }
}
