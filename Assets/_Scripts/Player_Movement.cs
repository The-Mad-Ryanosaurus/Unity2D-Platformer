using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
     // Speed at which the object moves

    [SerializeField]
    private float runSpeed = 10.0f;
    [SerializeField]
    public float jumpSpeed = 5.0f;
    [SerializeField]
    private float xBounds = 13.0f, yBounds = 7.0f;
    private PolygonCollider2D myFeet;
    public Rigidbody2D playerRB;
    bool facingRight = true;



    void start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<PolygonCollider2D>();
        Debug.Log("done start");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        
        float move = Input.GetAxisRaw("Horizontal");
        if(move<0 && facingRight)
        {
            Flip();
        }
        else if(move>0 && !facingRight)
        {
            Flip();
        }
    }

    public void Jump()
    {//if the boxcollider touches platform jump if not return
        if (myFeet == null) myFeet = GetComponent<PolygonCollider2D>();

        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("floor")))
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            Vector2 jumpVelToAdd = new Vector2(0f, jumpSpeed);
            playerRB.velocity += jumpVelToAdd;

        }
    }

    public void Move()
    {
        // Get the Horizontal and Vertical Inputs 
        float horizontal = Input.GetAxis("Horizontal");

        // Transfrom (respectively) The x (horizontal) and y (vertical) positions of an object using TimeDelta as the frames update 
        transform.position += new Vector3(horizontal * runSpeed * Time.deltaTime, 0);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xBounds, xBounds),
                                        Mathf.Clamp(transform.position.y, -yBounds, yBounds),
                                        transform.position.z);
    }

    public void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);

        
    }

}
