using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManagerSingleton : MonoBehaviour {
    public static PlayerManagerSingleton Instance { get; private set; }

    public enum LocomotionMode { Teleport, Immersive }
    public LocomotionMode SelectedMode { get; private set; } = LocomotionMode.Immersive; // Default mode
    //public LocomotionMode SelectedMode { get; private set; } = LocomotionMode.Teleport; // Default mode

    public OVRPlayerController PlayerController { get; private set; }
    public OVRCameraRig InicioPlayerController { get; private set; }

    public List<GameObject> TeleportControllers { get; private set; } = new List<GameObject>();

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes

            UpdatePlayerControllerReference();
        } else {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    public void SetLocomotionMode(LocomotionMode mode) {
        SelectedMode = mode;
        Debug.Log($"Locomotion mode set to: {SelectedMode}");
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        UpdatePlayerControllerReference();
    }

    private void UpdatePlayerControllerReference() {
        PlayerController = FindObjectOfType<OVRPlayerController>();

        if (PlayerController == null) {
            Debug.LogWarning("OVRPlayerController not found in the current scene.");
            InicioPlayerController = FindObjectOfType<OVRCameraRig>();
            SetTeleportControllers(true);
        } else {
            Debug.LogWarning($"OVRPlayerController FOUND: {PlayerController.name}.");
            SetCurrentPlayerControllerLocomotionMode();
        }
    }

    private void SetCurrentPlayerControllerLocomotionMode() {
        PlayerController.EnableLinearMovement = (SelectedMode == LocomotionMode.Immersive) ? true : false;
        
        /*
        TeleportControllers = FindAllNestedObjectsByName(PlayerController.transform, "ControllerTeleportInteractorGroup");
        if (TeleportControllers.Count != 0) {
            int auxCounter = 0;
            foreach (var TeleportController in TeleportControllers) {
                bool teleportState = (SelectedMode == LocomotionMode.Teleport) ? true : false;
                TeleportController.SetActive(teleportState);

                Debug.LogWarning($"TeleportControllers FOUND [{auxCounter}]: {TeleportController.name}");
                auxCounter++;
            }
        }
        */
        SetTeleportControllers(false);
    }

    private void SetTeleportControllers(bool isIntroScene) {
        // true: escena Inicio
        // false: escenas siguientes

        if (isIntroScene) {
            TeleportControllers = FindAllNestedObjectsByName(InicioPlayerController.transform, "LocomotionControllerInteractorGroup");
        } else {
            TeleportControllers = FindAllNestedObjectsByName(PlayerController.transform, "ControllerTeleportInteractorGroup");
        }

        if (TeleportControllers.Count != 0) {
            int auxCounter = 0;
            foreach (var TeleportController in TeleportControllers) {
                bool teleportState = (SelectedMode == LocomotionMode.Teleport) ? true : false;
                TeleportController.SetActive(teleportState);

                Debug.LogWarning($"TeleportControllers FOUND [{auxCounter}]: {TeleportController.name}");
                auxCounter++;
            }
        }
    }

    private List<GameObject> FindAllNestedObjectsByName(Transform parent, string targetName) {
        List<GameObject> matches = new List<GameObject>();

        foreach (Transform child in parent) {
            if (child.name == targetName) {
                matches.Add(child.gameObject);
            }

            // Recursively search deeper children
            matches.AddRange(FindAllNestedObjectsByName(child, targetName));
        }

        return matches;
    }
}
