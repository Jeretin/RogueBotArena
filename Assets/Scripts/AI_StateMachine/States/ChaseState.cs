using UnityEngine;
using UnityEngine.AI;

public class ChaseState : BaseState
{
    private NavMeshAgent agent;
    public override void EnterState(StateMachine stateMachine)
    {
        Debug.Log("Entering Chase State");
        agent = stateMachine.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
    }

    public override void Update(StateMachine stateMachine)
    {
        
        Vector3 playerPosition = stateMachine.player.position;
        agent.SetDestination(playerPosition);

        if (Vector3.Distance(stateMachine.transform.position, playerPosition) <= agent.stoppingDistance)
        {
            stateMachine.SwitchState(stateMachine.attackState);
        }
    }

    public override void ExitState(StateMachine stateMachine)
    {
        Debug.Log("Leaving Chase State");
        agent.isStopped = true; // Stop the agent
    }
}
