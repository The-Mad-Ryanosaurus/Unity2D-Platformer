using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Rigidbody2D player;

    // Records position of player spawn point(s)
    private Vector3 respawnPoint;
    // Links script to falldamage tag in scene. empty object follows player
    [SerializeField]
    public GameObject fallDamage;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Rigidbody2D>();    
        playerAnimation = GetComponent<Animator>();

        // Stores position of player at start of game. Respanws player back to this position when fall out of scene 
        respawnPoint = transform.position;
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

        // Moves fall damage empty object to follow player on x axis, y axis stays the same
        fallDamage.transform.position = new Vector2(transform.position.x, fallDamage.transform.position.y);



    }

    public void Jump()
    {
        // Finds the empty object (groundChecker) and checks the radius of it to see if touching ground and if the layer the player is on is ground
        isGrounded = Physics2D.OverlapCircle(groundedPlayerChecker.position, groundedPlayerRadius, groundLayer);

        //Debug.Log($"{groundedPlayerChecker.position},\t{groundedPlayerRadius}.\t{groundLayer}");

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
            transform.localScale = new Vector2(0.8f, 0.8f);
        }
        else if(direction < 0f)
        {
            player.velocity = new Vector2(direction * runSpeed, player.velocity.y);
            transform.localScale = new Vector2(-0.8f, 0.8f);
        }

    }

    //When player collides with DeathDetector -> respawn
    public void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("Got Here");
        if(collision.tag == "FallDamage")
        {
            // Debug.Log("And Here");
            transform.position = respawnPoint;
        }
        else if(collision.tag == "checkpoint")
        {
            respawnPoint = transform.position;
        }

        // Level Changes
        else if(collision.tag == "TowerEntrance")
        {
            // Scene Manager loads scene at index 1 for tag that = "NextLevel"
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
        else if(collision.tag == "TowerTop")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        }
    }


}
