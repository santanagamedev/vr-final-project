using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotRelicController : MonoBehaviour
{
    
    [SerializeField]
    private bool slotRelicEmpty = true;
    [SerializeField]
    
    private GameObject correctRelic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnTriggerEnter(Collider other) 
    {
        
        if ( other.gameObject.name == correctRelic.name)
        {
            Debug.Log(other.name);
            Debug.Log("Reliquia en el slot");
            slotRelicEmpty = false;
            
        }
    }

}
