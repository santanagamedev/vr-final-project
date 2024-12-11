using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMentorRoom : MonoBehaviour
{
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        PaintingManager.OnPuzzelSolved += HandlePuzzlesolved;
    }

    private void OnDisable() {
        PaintingManager.OnPuzzelSolved -= HandlePuzzlesolved;
    }

    private void HandlePuzzlesolved()
    {
        if (animator != null)
        {
            animator.SetBool("ThirdPuzzleIsSolved", true);
        }
        else
        {
            Debug.LogWarning("DoorMentorRoom: Animator no esta asignado");
        }
    }
    

}
