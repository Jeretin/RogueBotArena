using UnityEngine;

public class TauntState_Puncher : BaseState_Puncher
{  
    private float tauntTime = 2.0f;
    private float tauntTimer = 0.0f;

    public override void EnterState(FSM_Puncher stateMachine)
    {
        Debug.Log("Taunt State");
    }

    public override void Update(FSM_Puncher stateMachine)
    {
        if (stateMachine.previousState == stateMachine.chaseState){
            stateMachine.SwitchState(stateMachine.attackState);
        } else {
            stateMachine.SwitchState(stateMachine.chaseState);
        }
    }

    public override void ExitState(FSM_Puncher stateMachine)
    {
        Debug.Log("Exit Taunt");
    }

    public override void OnCollisionEnter(FSM_Puncher stateMachine, Collision collision)
    {
        throw new System.NotImplementedException();
    }
}
