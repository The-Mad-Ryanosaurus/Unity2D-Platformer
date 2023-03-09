using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    [SerializeField]
    public float runSpeed = 5f;
    // Direction == -1 for left, 0 for idle and +1 for right
    [SerializeField]
    public float jumpSpeed = 5f;
    public float direction = 0f;
    [SerializeField]
    public Transform groundedPlayerChecker;
    public float groundedPlayerRadius;
    public LayerMask groundLayer;
    public bool isGrounded;

    private Animator playerAnimation;

    [SerializeField] 
    public float xBounds = 46.16f, yBounds = 13.127f;

    [SerializeField]
    private Rigidbody2D player;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Rigidbody2D>();    
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

        // Get velocity of player on x axis
        // Mathf.Abs takes velocity on x axis and regardless of positive or negative (as direction is set) velocity and speed will still stay positive and player can move left
        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        // Checks if player is on the ground for animations
        playerAnimation.SetBool("Grounded", isGrounded);



    }

    public void Jump()
    {
        // Finds the empty object (groundChecker) and checks the radius of it to see if touching ground and if the layer the player is on is ground
        isGrounded = Physics2D.OverlapCircle(groundedPlayerChecker.position, groundedPlayerRadius, groundLayer);

        // If space is pressed AND the player is on ground, jump enabled
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }
    }

    public void Move()
    {
        direction = Input.GetAxis("Horizontal");
        //Debug.Log(direction);

        if(direction > 0f)
        {
            player.velocity = new Vector2(direction * runSpeed, player.velocity.y);
            // Direction Player Faces
            transform.localScale = new Vector2(0.5f, 0.5f);
        }
        else if(direction < 0f)
        {
            player.velocity = new Vector2(direction * runSpeed, player.velocity.y);
            transform.localScale = new Vector2(-0.5f, 0.5f);
        }

        // transform.position += new Vector3(direction * runSpeed * Time.deltaTime, 0);
        // transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xBounds, xBounds),
        //                                 Mathf.Clamp(transform.position.y, -yBounds, yBounds), 
        //                                 transform.position.z);



        
    }
}
