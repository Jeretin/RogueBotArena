using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 move;
    private Animator anim;
    private Rigidbody rb;
    private GameObject centerPoint;

    [SerializeField] private float speed = 5f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
    }

    private void Update(){
        move = playerControls.Gameplay.Move.ReadValue<Vector2>();
        // Player movement to direction of rotation
        Vector3 verticalMovement = transform.forward * move.y;
        Vector3 horizontalMovement = transform.right * move.x;

        Vector3 movement = (verticalMovement + horizontalMovement).normalized * speed * Time.deltaTime;

        anim.SetFloat("Speed", move.magnitude);

        rb.MovePosition(rb.position + movement);
        RotatePlayer();

        playerControls.Gameplay.Shoot.performed += ctx => Shoot();
        
    }

    private void RotatePlayer(){
        // Raycast to mouse position
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        // Player looks at mouse position
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookAt = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookAt);
        }

        // Draw ray
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red); 
    }

    private void Shoot(){
        Debug.Log("Shoot");
    }


}
