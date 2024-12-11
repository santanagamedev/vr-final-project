using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotRelicController : MonoBehaviour
{
    
    [SerializeField]
    public bool slotRelicEmpty = true;
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
            
            slotRelicEmpty = false;
            
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        
        if ( other.gameObject.name == correctRelic.name)
        {
            
            slotRelicEmpty = true;
            
        }
    }

}
