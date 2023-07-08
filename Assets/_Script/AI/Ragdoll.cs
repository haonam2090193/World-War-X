
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] rigidBodies;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        DeactivateRaggdoll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DeactivateRaggdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }
        animator.enabled = true; 
    }
    public void ActiveRaggdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
        }
        animator.enabled = false;
    }
    public void ApplyForce(Vector3 force, Rigidbody rigidbody)
    {
        rigidbody.AddForce(force, ForceMode.VelocityChange);
    }
}
