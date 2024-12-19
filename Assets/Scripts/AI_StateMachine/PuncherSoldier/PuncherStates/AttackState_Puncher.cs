using UnityEngine;

public class AttackState_Puncher : BaseState_Puncher
{
    public override void EnterState(FSM_Puncher stateMachine)
    {
        Debug.Log("Attack State");
        stateMachine.agent.SetDestination(stateMachine.player.position);

        stateMachine.anim.SetBool("isAttacking", true);
        stateMachine.agent.speed = stateMachine.attackSpeed;
        stateMachine.agent.stoppingDistance = 0.0f;

        
    }

    public override void Update(FSM_Puncher stateMachine)
    {
        if (stateMachine.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete){
            stateMachine.previousState = stateMachine.currentState;
            stateMachine.SwitchState(stateMachine.tauntState);
        } 
    }

    public override void ExitState(FSM_Puncher stateMachine)
    {
        Debug.Log("Exit Attack State");

        stateMachine.agent.speed = stateMachine.walkSpeed;
        stateMachine.agent.stoppingDistance = stateMachine.stoppingDistance;
        stateMachine.anim.SetBool("isAttacking", false);
    }

    public override void OnCollisionEnter(FSM_Puncher stateMachine, Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(stateMachine.damage);

            stateMachine.previousState = stateMachine.currentState;
            stateMachine.SwitchState(stateMachine.tauntState);
        }
    }
}
