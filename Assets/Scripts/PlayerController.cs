using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    #region Components
    private PlayerControls playerControls;
    private InputAction shootAction;
    private InputAction dashAction;
    private Vector2 move;
    private Animator anim;
    private Rigidbody rb;
    [SerializeField] private GameObject bulletSpawn;

    #endregion
    #region Player Settings

    [Header("Player Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 10f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 2f;

    [Header("Shooting Settings")]
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private GameObject bulletPrefab;

    // Private player variables
    private bool isShooting = false;
    private bool isDashing = false;
    private float shootTimer = 0f;
    private float dashTimer = 0f;
    private float realSpeed;

    #endregion


    #region Starting stuff
    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        shootAction = playerControls.Gameplay.Shoot;
        dashAction = playerControls.Gameplay.Dash;
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
        realSpeed = speed;
    }

    #endregion

    private void Update(){

        #region Input actions
        move = playerControls.Gameplay.Move.ReadValue<Vector2>();

        shootAction.started += ctx => {
            isShooting = true;
        };
        shootAction.canceled += ctx => {
            isShooting = false;
        };

        dashAction.performed += ctx => {
            if (dashTimer <= 0){
                Dash();
            } else {
                Debug.Log("Cooldown: " + dashTimer);
            }

        };
        
        #endregion

        #region Player movement
        // Player movement to direction of rotation
        Vector3 verticalMovement = transform.forward * move.y;
        Vector3 horizontalMovement = transform.right * move.x;

        Vector3 movement = (verticalMovement + horizontalMovement).normalized * realSpeed * Time.deltaTime;

        anim.SetFloat("Speed", move.magnitude);

        rb.MovePosition(rb.position + movement);
        RotatePlayer();

        #endregion

        // Shooting
        if(isShooting){
            if (shootTimer <= 0){
                Shoot();
                shootTimer = shootCooldown;
            }
            else{
            }
        }

        if (isDashing){
            if (dashTimer <= 0){
                isDashing = false;
                realSpeed = speed;
                dashTimer = dashCooldown;
            } else {
                dashTimer -= Time.deltaTime;
            }
        }
        dashTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;

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


    #region Mechanichs
    private void Shoot(){

        anim.SetTrigger("Shoot");

        // Instantiate bulletprefab to bulletspawn position
        Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);


        Debug.Log("is shooting");
    }

    private void Dash(){
        realSpeed = dashSpeed;
        isDashing = true;

        dashTimer = dashDuration;

        Debug.Log("Dashing");
    }

    public void TakeDamage(float damage){
        health -= damage;
        Debug.Log(" Player Health: " + health);
        if (health <= 0){
            gameObject.SetActive(false);
        }
    }

    #endregion

}
