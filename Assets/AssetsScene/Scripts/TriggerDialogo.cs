using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogo : MonoBehaviour
{
    private AudioManager voces;
    
    private void Start() 
    {
       voces = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Update() 
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Dialogo1") == false)
        {
            voces.PlayDialogo1();
            Destroy(gameObject);
        }
    }
}
