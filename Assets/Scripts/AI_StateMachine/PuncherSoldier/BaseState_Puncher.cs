using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public abstract class BaseState_Puncher
{
    public abstract void EnterState(FSM_Puncher stateMachine);
    public abstract void Update(FSM_Puncher stateMachine);
    public abstract void ExitState(FSM_Puncher stateMachine);

    public abstract void OnCollisionEnter(FSM_Puncher stateMachine, Collision collision);
}
