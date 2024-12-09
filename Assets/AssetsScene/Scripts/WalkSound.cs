using UnityEngine.XR;
using UnityEngine;


public class WalkSound : MonoBehaviour
{
    public AudioClip stepSound;
    private AudioSource audioSource; 
    private XRNode inputSource = XRNode.LeftHand; 
    private InputDevice device;
    private Vector3 lastPosition;
    private float moveThreshold = 0.1f; 
    private bool isWalking = false;

    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
        lastPosition = transform.position;
    }

    void Update()
    {

        if (!device.isValid)
        {
            return;
        }

        float distance = Vector3.Distance(lastPosition, transform.position);
        

        if (distance > moveThreshold && !isWalking)
        {
            PlayWalkingSound();
            isWalking = true;
        }
        else if (isWalking)
        {
            StopWalkingSound();
            isWalking = false;
        }

        lastPosition = transform.position; 
    }

    private void PlayWalkingSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = stepSound;
            audioSource.Play();
        }
    }

    private void StopWalkingSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
