using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerShipControl : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float fuel = 100f;
    [SerializeField] float burnFuelRate = .2f;

    Rigidbody2D rigidBody;
    bool canMove = true;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        fuel -= burnFuelRate * Time.deltaTime;

        if(fuel <= Mathf.Epsilon)
        {
            canMove = false;
        }

        if (canMove)
        {
            HandleMovement();
        }

    }

    void HandleMovement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0) // Move right
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) // Move left
        {
            rigidBody.velocity = new Vector2(-speed, rigidBody.velocity.y);
        }
        else if (Input.GetAxisRaw("Vertical") > 0) // Move up
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, speed);
        }
        else if (Input.GetAxisRaw("Vertical") < 0) // Move down
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -speed);
        }
        if (Input.GetAxisRaw("Horizontal") == 0) // No movement on horizontal axis
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") == 0) // No movement on vertical axis
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
        }
    }
}