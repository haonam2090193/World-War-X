using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AiAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateID initState;
    public NavMeshAgent navMeshAgent;
    void Start()
    {
        stateMachine = new AIStateMachine(this);
        stateMachine.ChangeState(initState);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }

    
}
