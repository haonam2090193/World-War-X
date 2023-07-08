using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStateID
{
    ChasePlayer
}
public interface AIState 
{
   AIStateID GetID();
    void Enter(AiAgent aiAgent);
    void Update(AiAgent aiAgent);
    void Exit(AiAgent aiAgent);
}
