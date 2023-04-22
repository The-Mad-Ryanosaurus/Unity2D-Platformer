using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Text scoreText;

    [SerializeField]
    private Animator Player_Sprite;
    private Animator Enemy_Sprite;

    [SerializeField]
    private string Player_Die = "Player_Die";
    private string Spider_Die = "Spider_Die";

    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Rigidbody2D>();    
        playerAnimation = GetComponent<Animator>();

        // Stores position of player at start of game. Respanws player back to this position when fall out of scene 
        respawnPoint = transform.position;

        // CONVERTS SCORE TO STRING TO DISPLAY IN UI 
        scoreText.text = "Score: "  +  Score.totalScore.ToString();
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

        if(HealthSystem.maxHealth <= 0)
        {
            StartCoroutine(LoadSceneAfterDelay());
        }
        IEnumerator LoadSceneAfterDelay()
        {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(3);
        }

        if(SceneManager.GetActiveScene().name == "DeathScreen")
        {
            Debug.Log("HELLO");
            HealthSystem.maxHealth = 6;
            Score.totalScore = 0;
        }

        
        

    }

    public void Jump()
    {
        if (!isAlive) return;
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
        if (!isAlive) return;
        direction = Input.GetAxis("Horizontal");

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


    // CODE FOR ALL COLLISIONS, DAMAGE/RESPAWN, SCENE CHANGES AND COIN COLLECTION/SCORE INCREASE 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDamage")
        {
            transform.position = respawnPoint;
        }
        else if(collision.tag == "checkpoint")
        {
            respawnPoint = transform.position;
        }

        // LEVEL/SCENE CHANGES
        else if(collision.tag == "NextLevel")
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
        

        // COIN COLLECTION AND SCORE INCREASE
        else if(collision.tag == "Coin")
        {
            // DOES NOT WORK FOR SCENE CHANGES
            // score += 1;
            // Debug.Log(score);
            
            
            // WORKS WITH SCENE CHANGES
            Score.totalScore += 1;
            //DISPLAYS SCORE IN UI 
            scoreText.text = "Score: "  +  Score.totalScore.ToString();
            // DISABLES COIN ON COLLISION
            collision.gameObject.SetActive(false);
        }
      
    }

    //PLAYER DAMAGE AND ENEMIES
     private void OnCollisionEnter2D(Collision2D collision) 
    {

        foreach(ContactPoint2D contact in collision.contacts)
        {
            if(contact.collider.CompareTag("Spikes"))
            {
                HealthSystem.maxHealth -= 6;

                if(HealthSystem.maxHealth <= 0)
                {
                    isAlive = false;
                    Player_Sprite.Play(Player_Die, 0, 0.0f);
                }
            }
        }
       
        foreach(ContactPoint2D contact in collision.contacts)
        {
            if(contact.collider.CompareTag("Spider"))
            {
                HealthSystem.maxHealth -= 2;

                if(HealthSystem.maxHealth <= 0)
                {
                    isAlive = false;
                    Player_Sprite.Play(Player_Die, 0, 0.0f);
                }
            }
        }
    }
    
}
