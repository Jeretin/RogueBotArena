using UnityEngine;
using UnityEngine.AI;

public class ChaseState_Puncher : BaseState_Puncher
{
    public override void EnterState(FSM_Puncher stateMachine)
    {
        Debug.Log("Chase State");
    }

    public override void Update(FSM_Puncher stateMachine)
    {
        if (Vector3.Distance(stateMachine.transform.position, stateMachine.player.position) >= stateMachine.distanceToAttack)
        {
            stateMachine.agent.SetDestination(stateMachine.player.position);
        }
        else
        {
            stateMachine.SwitchState(stateMachine.tauntState);
        }
    }

    public override void ExitState(FSM_Puncher stateMachine)
    {
        stateMachine.previousState = stateMachine.chaseState;
        Debug.Log("Exit Chase State");
    }

    public override void OnCollisionEnter(FSM_Puncher stateMachine, Collision collision)
    {
        throw new System.NotImplementedException();
    }
}
