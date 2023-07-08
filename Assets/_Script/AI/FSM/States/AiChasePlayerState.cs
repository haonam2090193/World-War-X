using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiChasePlayerState : AIState
{
    public Transform player;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    private float timer = 0f;

    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;
    }

    public void Enter(AiAgent aiAgent)
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

        }
    }

    public void Update(AiAgent aiAgent)
    {
        if (!aiAgent.enabled)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (!aiAgent.navMeshAgent.hasPath)
        {
            aiAgent.navMeshAgent.destination = player.position;
        }
        if (timer < 0f)
        {
            Vector3 direction = player.position - aiAgent.navMeshAgent.destination;
            direction.y = 0;
            if (direction.sqrMagnitude > maxDistance * maxDistance)
            {
                if (aiAgent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    aiAgent.navMeshAgent.destination = player.position;
                }
            }
            timer = maxTime;
        }
        if (aiAgent.navMeshAgent.hasPath)
          //  aiAgent.animator.SetFloat("Speed", aiAgent.navMeshAgent.velocity.magnitude);

        throw new System.NotImplementedException();
    }

    public void Exit(AiAgent aiAgent)
    {
        throw new System.NotImplementedException();
    }


}
