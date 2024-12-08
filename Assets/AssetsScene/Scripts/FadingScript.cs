using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadingScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 5.0f;
    [SerializeField] private GameObject colliderLetter;
    private AudioSource voicesClip;
    private bool canPlay = true;

    private void Start() {
        voicesClip = GetComponent<AudioSource>();
    }

    
    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0, fadeDuration));
    }

    public void FadeOut()
    {
        if (canPlay)
        {
            canPlay = false;
            StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, fadeDuration)) ;
        }
        else
        {
            Debug.Log("Fade is running");
        }
    }

    private void Update() {
        Debug.Log(canPlay);
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {                 
        voicesClip.PlayOneShot(voicesClip.clip);    
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }
        cg.alpha = end;
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(2);  
        
       
    }

    public void DisableLetterCollider()
    {
        colliderLetter.GetComponent<Collider>().enabled = false;
    }
}
