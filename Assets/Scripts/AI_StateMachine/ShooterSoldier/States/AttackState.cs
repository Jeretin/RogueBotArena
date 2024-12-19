using UnityEngine;

public class AttackState : BaseState
{
    public override void EnterState(StateMachine stateMachine)
    {
        Debug.Log("Entering Attack State");
    }

    public override void ExitState(StateMachine stateMachine)
    {
        Debug.Log("Leaving Attack State");
    }

    public override void Update(StateMachine stateMachine)
    {
        // Rotate towards player
        Vector3 direction = stateMachine.player.position - stateMachine.transform.position;
        stateMachine.transform.rotation = Quaternion.LookRotation(direction);

        if (stateMachine.shootTimer <= 0){
            stateMachine.gameObject.GetComponent<SoldierAbilities>().ShootAtPlayer();   // Call the ShootAtPlayer method
            stateMachine.shootTimer = stateMachine.shootCooldown;
        }
        else{
            stateMachine.shootTimer -= Time.deltaTime;
        }


        if (Vector3.Distance(stateMachine.transform.position, stateMachine.player.position) > stateMachine.attackRange)
        {
            stateMachine.SwitchState(stateMachine.chaseState);
        }
    }
}
