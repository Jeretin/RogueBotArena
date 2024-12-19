
using Unity.VisualScripting;

public abstract class BaseState{
    public abstract void EnterState(StateMachine stateMachine);
    public abstract void Update(StateMachine stateMachine);
    public abstract void ExitState(StateMachine stateMachine);
}
