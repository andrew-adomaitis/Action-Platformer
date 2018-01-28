using System.Collections;
using UnityEngine;
using UnityEngine.UI;
// TODO find a way to check if all bullets have hit a single enemy. Try an array on the enemy?
[RequireComponent(typeof(Rigidbody2D), typeof(HealthSystem))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [Range(1, 10)][SerializeField] float maxSpeed = 5f;
    [Range(1, 10)][SerializeField] float jumpForce = 5f;

    [Header("Jumping")]
    [Tooltip("Multiplies how fast the player falls for better feel")]
    [SerializeField] float fallMultiplier = 2f;
    [Tooltip("How high a low jump is")]
    [SerializeField] float lowJumpMultiplier = 1.5f;
    [Tooltip("A empty child that will be placed at the player's feet")]
    [SerializeField] Transform groundCheck;
    [Tooltip("How far to check for the ground")]
    [SerializeField] float groundCheckRadius = .2f;
    [SerializeField] bool isGrounded = true;

    [Header("Knockback")]
    [Tooltip("How far to knock the player along the X axis")]
    public float hurtKnockbackForceLengthwise = 5f;
    [Tooltip("How far to knock the player along the Y axis")]
    public float hurtKnockbackForceHeight = 2f;
    [Tooltip("How long the knockback lasts")]
    public float hurtKnockbackLength = .1f;
    [Tooltip("How long the player will be invincible for")]
    [SerializeField] float invincibilityTime = 1f;

    [Header("Layers/Tags")]
    [SerializeField] LayerMask groundLayer;

    [Header("Attacking")]
    [Tooltip("The image that will show how close the gun is to being able to shoot again")]
    [SerializeField] Image reloadProgressImage;
    [SerializeField] GameObject bulletHolder;
    [SerializeField] GameObject bulletPrefab;
    [Tooltip("How much of an angle do shoot the bullets in")]
    [SerializeField] float bulletSpreadAngle = 45;
    public float damage = 10f;
    [SerializeField] int numOfBullets = 5;
    [SerializeField] Transform gunEnd;
    public float bulletSpeed = 10f;
    [SerializeField] float timeBetweenHits = 1f;
    
    [Header("Camera Shake")]
    [Tooltip("How long to shake the camera when the player gets hurt")]
    public float hurtCamShakeLength = .1f;
    [Tooltip("How hard to shake the camera when the playet gets hurt")]
    public float hurtCamShakeIntensity = .15f;
    [SerializeField] float shakeFrequency = .1f;
    
    [HideInInspector] public Vector3 respawnPos;
    [HideInInspector] public bool canMove = true;
    
    const string GROUNDED_BOOLEAN = "Grounded";
    const string SPEED_FLOAT = "Speed";
    const string ATTACK_TRIGGER = "Attack";

    Collider2D playerCollider;
    Animator anim;
    Rigidbody2D rb;
    GameController gameController;
    GameObject weaponObject;
    HealthSystem healthSystem;
    BaseEnemy enemy;
    CameraShake cameraShake;
    CameraRig mainCameraScript;

    float invincibilityLength;
    float originalTimeForHits;
    float originalGravityScale;
    bool canAttack = true;
        
    void Start()
    {
        originalTimeForHits = timeBetweenHits;
        respawnPos = transform.position;
        FindComponents();
        originalGravityScale = rb.gravityScale;
    }

    void Update()
    {
        if (canMove)
        {
            HandleMovement();
        }

        if (invincibilityLength > Mathf.Epsilon)
        {
            invincibilityLength -= Time.deltaTime;

        }
    }

    void FixedUpdate()
    {
        HandleLowHighJump();
    }

    void HandleLowHighJump()
    {
        if (rb.velocity.y < 0) // When the player starts falling
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) // If the player is in the middle of jumping
        {
            rb.gravityScale = lowJumpMultiplier; // Start the low jump
        }
        else
        {
            rb.gravityScale = originalGravityScale; // Reset the gravity scale
        }
    }

    void HandleMovement()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        anim.SetBool(GROUNDED_BOOLEAN, isGrounded);

        // Move right
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            anim.SetFloat(SPEED_FLOAT, 2f);
            transform.localScale = new Vector2(1, 1);
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        // Move left
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            anim.SetFloat(SPEED_FLOAT, 2f);
            transform.localScale = new Vector2(-1, 1);
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
        // No movement
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
            anim.SetFloat(SPEED_FLOAT, 0);
        }
        // Attack
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            anim.SetTrigger(ATTACK_TRIGGER);
            ShootWeapon();
        }
        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void ShootWeapon()
    {        
        for (float j = -bulletSpreadAngle; j < bulletSpreadAngle; j += bulletSpreadAngle / numOfBullets * 2)
        {
            GameObject bullet = Instantiate(bulletPrefab, gunEnd.position, Quaternion.identity); // Instantiate a bullet
            Rigidbody2D projectileRigidbody = bullet.GetComponent<Rigidbody2D>(); // Get the bullet's Rigidbody
            bullet.transform.localRotation = new Quaternion(Quaternion.identity.x, Quaternion.identity.y, j, Quaternion.identity.w);

            // Check for which direction the player's facing and fire the bullets accordingly
            if (transform.localScale.x < 0 && bulletSpeed < 0)
                bulletSpeed *= -1;
            else if (transform.localScale.x > 0 && bulletSpeed > 0)
                bulletSpeed *= -1;
        }
        StartCoroutine(CountDownTimeBetweenHits());
    }

    void FindComponents()
    {
        mainCameraScript = FindObjectOfType<CameraRig>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gameController = FindObjectOfType<GameController>();
        healthSystem = GetComponent<HealthSystem>();
        cameraShake = gameController.gameObject.GetComponent<CameraShake>();
        playerCollider = GetComponent<Collider2D>();
    }

    public IEnumerator CountDownTimeBetweenHits()
    {
        reloadProgressImage.fillAmount = 0;
        canAttack = false;
        while (timeBetweenHits > Mathf.Epsilon)
        {
            reloadProgressImage.gameObject.SetActive(true);
            timeBetweenHits -= Time.deltaTime;
            reloadProgressImage.fillAmount += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        canAttack = true;
        reloadProgressImage.gameObject.SetActive(false);
        timeBetweenHits = originalTimeForHits;
    }
    
    void OnDrawGizmos()
    {
        // Draw a sphere that shows where the groundCheck will check for ground layer
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gunEnd.position, .05f);
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Killzone")
        {
            gameController.Respawn();
        }
        else if (other.tag == "Enemy" && invincibilityLength <= Mathf.Epsilon) // If the player collides with an enemy
        {
            invincibilityLength = invincibilityTime;
            enemy = other.gameObject.GetComponent<BaseEnemy>(); // Find the enemy
            healthSystem.TakeDamage(enemy.damage); // Damage the player
            cameraShake.Shake(hurtCamShakeIntensity, hurtCamShakeLength, shakeFrequency); // Shake the camera
        }
        else if(other.tag == "Hazard" && invincibilityLength <= Mathf.Epsilon) // If the player collides with a hazard
        {
            invincibilityLength = invincibilityTime;
            Hazard hazard = other.gameObject.GetComponent<Hazard>(); // Find the hazard
            healthSystem.TakeDamage(hazard.damage); // Damage the player
            cameraShake.Shake(hurtCamShakeIntensity, hurtCamShakeLength, shakeFrequency); // Shake the camera
        }
     }

    void OnBecameInvisible()
    {
        if (transform.position.y > mainCameraScript.transform.position.y)
        {
            mainCameraScript.ResetXPos();
        }
    }
}