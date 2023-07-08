using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    public Transform playerPoision;

    private NavMeshAgent meshAgent;

    private void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        meshAgent.destination = playerPoision.position;
    }

}
