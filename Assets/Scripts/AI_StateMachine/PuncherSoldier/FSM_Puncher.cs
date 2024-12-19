using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Puncher : MonoBehaviour
{
    public BaseState_Puncher currentState;
    public BaseState_Puncher previousState;
    public ChaseState_Puncher chaseState = new ChaseState_Puncher();
    public AttackState_Puncher attackState = new AttackState_Puncher();
    public TauntState_Puncher tauntState = new TauntState_Puncher();

    [HideInInspector] public Animator anim;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Transform player;

    public float attackRange = 2.0f;
    public float walkSpeed = 2.0f;
    public float attackSpeed = 3.0f;
    public float stoppingDistance = 1.0f;
    public float damage = 2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stoppingDistance = agent.stoppingDistance;

        SwitchState(chaseState);    // Start in the chase state
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update(this);
    }

    public void SwitchState(BaseState_Puncher newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
