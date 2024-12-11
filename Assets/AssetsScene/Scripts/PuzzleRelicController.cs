using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using UnityEngine;

public class PuzzleRelicController : MonoBehaviour
{
     public GameObject[] respawns;
    // Start is called before the first frame update
    private bool stateRelic1, stateRelic2, stateRelic3;
    void Start()
    {
      
    }
    
    
    private void FixedUpdate() {
         StatePuzzle();
    }

    void LoadStateRelics()
    {
        stateRelic1 = respawns[0].GetComponent<SlotRelicController>().slotRelicEmpty;
        stateRelic2 = respawns[1].GetComponent<SlotRelicController>().slotRelicEmpty;
        stateRelic3 = respawns[2].GetComponent<SlotRelicController>().slotRelicEmpty;
    }
    void StatePuzzle()
    {
        LoadStateRelics();
        if (!stateRelic1 && !stateRelic2 && !stateRelic3)
        {
            Debug.Log("las reliquias estan en las posiciones correctas");
        }
    }
}
