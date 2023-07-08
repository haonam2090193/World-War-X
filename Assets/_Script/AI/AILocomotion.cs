using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    public NavMeshAgent meshAgent;
    private Animator animator;


    private void Start()
    {
        
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();

    }
    private void Update()
    {
        if (meshAgent.hasPath)
        {

        }
    }

}
