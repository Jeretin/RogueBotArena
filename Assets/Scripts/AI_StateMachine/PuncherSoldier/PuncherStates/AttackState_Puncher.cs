using UnityEngine;

public class AttackState_Puncher : BaseState_Puncher
{
    Vector3 dashDirection;
    float dashTimer = 0.0f;

    public override void EnterState(FSM_Puncher stateMachine)
    {
        Debug.Log("Attack State");
        dashTimer = 0.0f;
        stateMachine.agent.isStopped = true; 
        stateMachine.agent.updatePosition = false; 

        dashDirection = (stateMachine.player.position - stateMachine.transform.position).normalized;
    }

    public override void Update(FSM_Puncher stateMachine)
    {
        if (dashTimer < stateMachine.dashDuration){
            stateMachine.transform.position += dashDirection * stateMachine.dashSpeed * Time.deltaTime;
            dashTimer += Time.deltaTime;
        } else {
            stateMachine.SwitchState(stateMachine.PatrolState);
        }
    }

    public override void ExitState(FSM_Puncher stateMachine)
    {
        stateMachine.previousState = stateMachine.attackState;

        stateMachine.agent.Warp(stateMachine.transform.position);

        stateMachine.agent.isStopped = false;
        stateMachine.agent.updatePosition = true;

        Debug.Log("Exit Attack State");
    }

    public override void OnCollisionEnter(FSM_Puncher stateMachine, Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(stateMachine.damage);
        }
    }
}
