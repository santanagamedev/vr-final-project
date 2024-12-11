using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PaintingManager : MonoBehaviour {
    [SerializeField] private List<PaintingRotation> paintings;
    [SerializeField] private bool floatingState = false;

    [SerializeField] private Transform ceiling;
    private bool ceilingMoved = false;

    [Header("Audio")]
    public AudioSource audioSource;
    public bool playAudio = false;
    float intervalPlayVoices = 10.0f;
    private Coroutine audioCoroutine;

    public static event Action OnPuzzelSolved;

    private bool isPuzzelSolved = false;
    public bool IsPuzzelSolved
    {
        get => isPuzzelSolved;
        set
        {
            if (isPuzzelSolved != value)
            {
                isPuzzelSolved = value;
                if (isPuzzelSolved)
                {
                    OnPuzzelSolved?.Invoke();
                    Debug.Log("Cambio el boo, deberia haberse habierot la puta puerta");
                }
            }
        }
    }


    //[SerializeField] private GameObject paintingsParent;

    private void Awake() {
        // Encuentra todas las paintings activas en la escena
        paintings = new List<PaintingRotation>(FindObjectsOfType<PaintingRotation>());
    }

    private void Start() {
        audioSource = GameObject.Find("AudioPuzzel03").GetComponent<AudioSource>();
    }
    void Update() {
        if (!ceilingMoved) AllSetFloating(floatingState);
        
        if (floatingState) {
            if (!ceilingMoved) MoveCeilingUp();
            playAudio = true;
            CheckPaintingStates();
        }

        if (playAudio && audioCoroutine == null)
        {
            audioCoroutine = StartCoroutine(PlayAduioIntervals());
        }
        else if (!playAudio && audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
            audioCoroutine = null;
        }
    }

    public void SolvePuzzle()
    {
        IsPuzzelSolved = true;
    }

    private void CheckPaintingStates() {
        List<PaintingRotation> incorrectlyOriented = new List<PaintingRotation>();

        foreach (var painting in paintings) {
            bool shouldFaceWall = painting.GetShouldFaceWall();
            bool isFacingWall = painting.CheckIfFacingWall();

            if (shouldFaceWall != isFacingWall) {
                incorrectlyOriented.Add(painting);
            }
        }

        // Process the incorrectly oriented paintings
        foreach (var painting in incorrectlyOriented) {
            Debug.LogWarning($"{painting.name} is incorrectly oriented!");
        }

        if (incorrectlyOriented.Count == 0) {
            Debug.Log("All paintings are correctly oriented.");
            floatingState = false;
            AllSetFloating(floatingState);
            
            // Se abre la puerta que lleva al maestro
            SolvePuzzle();
            Debug.Log("Var de Painting Manager " + isPuzzelSolved);
            playAudio = false;
        }
        
    }

    private void AllSetFloating(bool floatingState) {
        foreach (var painting in paintings) {
            painting.SetFloating(floatingState);
        }
    }

    private void MoveCeilingUp() {
        //ceiling.position = new Vector3(ceiling.position.x, ceiling.position.y + 6, ceiling.position.z);
        ceiling.position = new Vector3(ceiling.position.x, 9.96f, ceiling.position.z);

        ceilingMoved = true;
    }

    IEnumerator PlayAduioIntervals()
    {
        while(playAudio)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        yield return new WaitForSeconds(intervalPlayVoices);
        }
    }
}

