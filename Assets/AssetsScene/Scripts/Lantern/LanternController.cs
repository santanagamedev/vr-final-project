using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour {
    [SerializeField] private GrabInteractable lanternInteractable;

    private const string SPOTLIGHT = "Spot Light";
    private const string GLASSLIGHT_ON = "LightOn";
    private const string GLASSLIGHT_OFF = "LightOff";

    [SerializeField] private bool isLightOn = false;
    

    // Update is called once per frame
    void Update() {
        // returns true if the primary button (typically “A”) was pressed this frame.
        if (lanternInteractable.State == InteractableState.Select) {
            Transform spotlight = lanternInteractable.transform.parent.Find(SPOTLIGHT);
            Transform lanternGlassOFF = lanternInteractable.transform.parent.Find(GLASSLIGHT_OFF);
            Transform lanternGlassON = lanternInteractable.transform.parent.Find(GLASSLIGHT_ON);

            if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three)) {
                Debug.LogWarning("Button A pressed.");
                isLightOn = !isLightOn;

                // turn spotlight on
                spotlight.gameObject.SetActive(isLightOn);

                // switch glasses (LightOn - LightOff)
                lanternGlassOFF.gameObject.SetActive(!isLightOn);
                lanternGlassON.gameObject.SetActive(isLightOn);

                // change button's color

            }   
        } else {
            Debug.LogWarning("NOT grabbed Yet");
        }

    }

}
