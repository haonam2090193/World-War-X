using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    private Animator animator;
    private Vector2 userInput;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        userInput.x = Input.GetAxis("Horizontal");
        userInput.y = Input.GetAxis("Vertical");

        animator.SetFloat("InputX",userInput.x);
        animator.SetFloat("InputY", userInput.y);

    }

}
