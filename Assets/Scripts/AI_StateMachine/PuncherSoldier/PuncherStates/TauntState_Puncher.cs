using UnityEngine;

public class TauntState_Puncher : BaseState_Puncher
{  
    private float tauntTime = 2.6f;
    private float tauntTimer = 0.0f;

    public override void EnterState(FSM_Puncher stateMachine)
    {
        Debug.Log("Taunt State");

        stateMachine.agent.isStopped = true;
        stateMachine.anim.SetTrigger("taunt");
    }

    public override void Update(FSM_Puncher stateMachine)
    {
        tauntTimer += Time.deltaTime;

        if (tauntTimer >= tauntTime){
                if (stateMachine.previousState == stateMachine.chaseState){
                    stateMachine.SwitchState(stateMachine.attackState);
                } else {
                    stateMachine.SwitchState(stateMachine.chaseState);
                }
        }
    }

    public override void ExitState(FSM_Puncher stateMachine)
    {
        Debug.Log("Exit Taunt");

        stateMachine.agent.isStopped = false;
        tauntTimer = 0.0f;
    }

    public override void OnCollisionEnter(FSM_Puncher stateMachine, Collision collision)
    {
        throw new System.NotImplementedException();
    }
}
