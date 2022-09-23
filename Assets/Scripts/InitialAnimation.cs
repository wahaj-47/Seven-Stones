using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialAnimation : MonoBehaviour
{
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }
    public void Throw(){
        animator.SetTrigger("Throw");
    }
}
