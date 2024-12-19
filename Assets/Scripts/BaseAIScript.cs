using UnityEngine;
using UnityEngine.AI;

public class BaseAIScript : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform target;
    private Animator anim;
    private bool isShooting = false;
    private float shootTimer = 0f;
    [SerializeField] private float shootCooldown = 0.5f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        MoveToTarget(target.position);
        anim.SetFloat("Speed", agent.velocity.magnitude);

        // Shooting

        if (shootTimer <= 0){
            Shoot();
            shootTimer = shootCooldown;
        }
        else{
            shootTimer -= Time.deltaTime;
        }

        
    }

    private void MoveToTarget(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void Shoot(){

        // Instantiate bulletprefab to bulletspawn position
        Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);


        Debug.Log("is shooting");
    }
}
