using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingRotation : MonoBehaviour {

    private float xMinAngle = 355;
    private float xMaxAngle = 6;
    private float yMinAngle = 80;
    private float yMaxAngle = 95;

    [SerializeField] private bool isFacingWall = false;
    [SerializeField] private bool shouldFaceWall = false;

    private float _floatingSpeed = 1.3f; // Speed for floating
    private float floatingAmplitude = 0.0015f;
    private float randomSpeed;

    private bool isFloating = false;

    void Start () { 
        randomSpeed = Random.Range(0.0f, 0.4f);
    }

    void Update() {
        if (isFloating) { 
            CheckIfFacingWall();

            // Calculate sinuous offset for up-down motion
            float sinOffset = Mathf.Sin(Time.time * (_floatingSpeed + randomSpeed)) * floatingAmplitude; // Amplitude of 0.5 units

            transform.position = new Vector3(transform.position.x,
                                             transform.position.y + sinOffset,
                                             transform.position.z);
        }
    }

    public bool CheckIfFacingWall() {
        Vector3 paintingRotation = transform.rotation.eulerAngles;
        //Debug.Log(paintingRotation);

        if (IsAngleInRange(paintingRotation.x, xMinAngle, xMaxAngle) &&
            IsAngleInRange(paintingRotation.y, yMinAngle, yMaxAngle)) {

            //Debug.LogWarning($"IN RANGEEEEE!  {paintingRotation.x}, {paintingRotation.y} ");
            isFacingWall = true;
        } else {
            isFacingWall = false;
        }

        return isFacingWall;
    } 

    public bool GetShouldFaceWall() {
        return shouldFaceWall;
    }

    private bool IsAngleInRange(float angle, float min, float max) {
        if (min < max) {
            // Standard range (e.g., 75 to 95)
            return angle > min && angle < max;
        } else {
            // Wrap-around range (e.g., 135 to 6)
            return angle > min || angle < max;
        }
    }

    public void SetFloating(bool floatingState) {
        isFloating = floatingState;
    }
}
