using UnityEngine;

public class TauntState_Puncher : BaseState_Puncher
{  
    private float tauntTime = 2.0f;
    private float tauntTimer = 0.0f;

    public override void EnterState(FSM_Puncher stateMachine)
    {
        Debug.Log("Taunt State");
        tauntTimer = 0.0f;
    }

    public override void Update(FSM_Puncher stateMachine)
    {
        tauntTimer += Time.deltaTime;

        if (tauntTimer >= tauntTime)
        {
            if (stateMachine.previousState is ChaseState_Puncher)
        {
            stateMachine.SwitchState(stateMachine.attackState);
        }
        else    // if (stateMachine.previousState is AttackState_Puncher)
        {
            stateMachine.SwitchState(stateMachine.chaseState);
        }
        }


        
    }

    public override void ExitState(FSM_Puncher stateMachine)
    {
        Debug.Log("Exit Taunt");
        tauntTimer = 0.0f;
    }

    public override void OnCollisionEnter(FSM_Puncher stateMachine, Collision collision)
    {
        throw new System.NotImplementedException();
    }
}
