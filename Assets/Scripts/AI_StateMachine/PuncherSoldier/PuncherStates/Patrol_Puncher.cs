using UnityEngine;
using UnityEngine.AI;

public class Patrol_Puncher : BaseState_Puncher
{
    public override void EnterState(FSM_Puncher stateMachine)
    {
        Debug.Log("Patrol State");

        Vector3 randomPoint = GetRandomPoint(stateMachine.transform.position, stateMachine.randomRoamRadius);
        if (randomPoint != Vector3.zero){
            stateMachine.agent.SetDestination(randomPoint);
        } else {
            stateMachine.SwitchState(stateMachine.chaseState);
        }
    }

    public override void Update(FSM_Puncher stateMachine)
    {
        if (stateMachine.agent.remainingDistance <= 0.5f){
            stateMachine.SwitchState(stateMachine.chaseState);
        }
    }

    public override void ExitState(FSM_Puncher stateMachine)
    {
        Debug.Log("Exit Patrol State");
    }

    public override void OnCollisionEnter(FSM_Puncher stateMachine, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    private Vector3 GetRandomPoint(Vector3 origin, float radius){
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas)){
            return hit.position;
        }

        return Vector3.zero;
    }
}
