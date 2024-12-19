using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private BaseState currentState;

    public ChaseState chaseState = new ChaseState();
    public AttackState attackState = new AttackState();

    [HideInInspector] public Transform player;
    public float attackRange = 5f;

    [HideInInspector] public float shootTimer = 0f;
    public float shootCooldown = 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        SwitchState(chaseState);    // Start in the chase state
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", agent.GetComponent<NavMeshAgent>().velocity.magnitude);


        currentState.Update(this);
    }

    public void SwitchState(BaseState newState){
        Debug.Log("Switching to " + newState);
        currentState?.ExitState(this);  // If currentState is not null, call ExitState
        currentState = newState;        // Set currentState to newState
        currentState.EnterState(this);  // Call EnterState on the new state
    }
}
