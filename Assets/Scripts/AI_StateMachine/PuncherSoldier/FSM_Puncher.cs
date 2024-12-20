using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class FSM_Puncher : MonoBehaviour
{
    public BaseState_Puncher currentState;
    public BaseState_Puncher previousState;
    public ChaseState_Puncher chaseState = new ChaseState_Puncher();
    public AttackState_Puncher attackState = new AttackState_Puncher();
    public TauntState_Puncher tauntState = new TauntState_Puncher();
    public Patrol_Puncher PatrolState = new Patrol_Puncher();

    [HideInInspector] public Animator anim;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Transform player;

    [HideInInspector] public Transform self;

    [Header("Movement")]
    [Tooltip("Radius within character finds a new destination point")]
    public float randomRoamRadius = 10.0f;
    [Tooltip("How close the character needs to be to the target to start attacking")]
    public float distanceToAttack = 7.0f;

    [Header("Attack")]
    public float damage = 2.0f;
    public float dashDuration = 1.0f;
    public float dashSpeed = 10.0f;

    void Awake(){
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        self = transform;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchState(chaseState);
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

    public void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }
}
