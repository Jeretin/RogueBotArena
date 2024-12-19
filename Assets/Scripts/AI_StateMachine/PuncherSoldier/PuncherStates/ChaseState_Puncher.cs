using UnityEngine;
using UnityEngine.AI;

public class ChaseState_Puncher : BaseState_Puncher
{
    public override void EnterState(FSM_Puncher stateMachine)
    {
        Debug.Log("Chase State");

        stateMachine.anim.SetBool("isChasing", true);
        stateMachine.agent.speed = stateMachine.walkSpeed;
    }

    public override void Update(FSM_Puncher stateMachine)
    {
        stateMachine.agent.SetDestination(stateMachine.player.transform.position);

        if (stateMachine.agent.remainingDistance <= stateMachine.attackRange)
        {
            stateMachine.previousState = stateMachine.currentState;
            stateMachine.SwitchState(stateMachine.tauntState);
        }
    }

    public override void ExitState(FSM_Puncher stateMachine)
    {
        Debug.Log("Exit Chase State");
        stateMachine.anim.SetBool("isChasing", false);
    }

    public override void OnCollisionEnter(FSM_Puncher stateMachine, Collision collision)
    {
        throw new System.NotImplementedException();
    }
}
