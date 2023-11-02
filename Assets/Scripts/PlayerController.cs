using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    public int MaxHealth { get; private set; } = 100;
    [field: SerializeField]
    public int Health { get; private set; } = 100;

    public float moveSpeed = 5f;   // Character movement speed
    public float jumpForce = 10f;  // Jump force
    public LayerMask groundLayer;  // Layer for detecting ground

    private Rigidbody2D rb;
    private bool isGrounded;

    private Transform groundCheck;
    private float groundCheckRadius = 0.2f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck"); // Create an empty GameObject as a child called "GroundCheck" and position it at the character's feet
    }

    private void Start()
    {
        Health = MaxHealth;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput, 0);
        
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void Damage(int amount)
    {
        Health -= amount;
        if (Health < 0)
            Die();
    }

    private void Die()
    {
        Debug.LogWarning("I Died!");
        //TODO: Death logic if there's time.
    }
}
