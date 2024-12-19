using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Rigidbody rb;
    protected GameObject objectToDestroy;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifetime = 2f;
    [SerializeField] protected float damage = 1f;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        // Destroy the col
    }
}
