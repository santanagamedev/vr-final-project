using UnityEngine.XR;
using UnityEngine;
using UnityEngine.InputSystem;


public class WalkSound : MonoBehaviour
{
    public AudioClip movingSound;  // Clip de sonido que se reproducirá en loop
    public float movementThreshold = 0f;  // Umbral de movimiento para detectar si el jugador se está moviendo

    private AudioSource audioSource;
    private Vector3 lastPosition;

    void Start()
    {
        // Inicializamos el AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configuramos el AudioSource para que se repita (loop) y no se reproduzca automáticamente
        audioSource.loop = true;
        audioSource.clip = movingSound;
        audioSource.playOnAwake = false; // No se reproduce inmediatamente al iniciar

        // Guardamos la posición inicial del jugador
        lastPosition = transform.position;
    }

    void Update()
    {
        // Verificamos el movimiento comparando la posición actual con la anterior
        float distanceMoved = Vector3.Distance(lastPosition, transform.position);

        if (distanceMoved > movementThreshold)
        {
            // Si el jugador se mueve, aseguramos que el sonido esté en loop y se reproduzca
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Si el jugador no se mueve, detenemos el sonido
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // Actualizamos la posición para el siguiente frame
        lastPosition = transform.position;
    }
}
