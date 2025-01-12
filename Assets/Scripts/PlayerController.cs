using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Components
    private PlayerControls playerControls;
    private InputAction shootAction;
    private InputAction dashAction;
    private InputAction pauseAction;
    private Vector2 move;
    private Animator anim;
    private Rigidbody rb;
    [SerializeField] private GameObject bulletSpawn;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Slider healthBar;

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
        pauseAction = playerControls.Gameplay.Pause;
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

        pauseAction.performed += ctx => {
            gameManager.PauseGame();
        };
        
        #endregion

        #region Player movement
        

        #region Player movement depending on rotation (not used)
        /*
        Vector3 verticalMovement = transform.forward * move.y;
        Vector3 horizontalMovement = transform.right * move.x;

        Vector3 movement = (verticalMovement + horizontalMovement).normalized * realSpeed * Time.deltaTime;

        anim.SetFloat("Speed", move.magnitude);

        rb.MovePosition(rb.position + movement);

        */

        #endregion

        #region Player movement not depending on rotation (used)
        // Player movement not depending on rotation
        Vector3 movement = new Vector3(move.y, 0, move.x) * realSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        RotatePlayer();

        #endregion

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

        gameManager.AddBulletShot();
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
        // if healthbar is not null, update it
        if (healthBar != null){
            healthBar.value = health/10;
        }
        Debug.Log(" Player Health: " + health);
        if (health <= 0){
            gameManager.PlayerDied();
            Time.timeScale = 0;
        }
    }

    #endregion

}
