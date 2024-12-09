using UnityEngine;

public class TriggerDialogo2 : MonoBehaviour
{
    private AudioManager voces;
    
    private void Start() 
    {
       voces = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Dialogo2") == false)
        {
            voces.PlayDialogo2();
            Destroy(gameObject);
        }
    }
}