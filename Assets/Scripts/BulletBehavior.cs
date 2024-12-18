using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 2f;

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

    private void OnTriggerEnter(Collider col){

        // TODO: Kill enemy

    }
}
