using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float jumpAmount = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Color32 dieColor = new Color32 (1, 1, 1, 1);
    [SerializeField] Vector2 deathKick = new Vector2 (0f, 5f);
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float startGravity;
    public bool isAlive = true;
    SpriteRenderer mySpriteRenderer;
    private CinemachineImpulseSource myImpulseSource;
    
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();  
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        startGravity = myRigidBody.gravityScale;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myImpulseSource = GetComponent<CinemachineImpulseSource>();
        
    }


    void Update()
    {
        if(!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if(!isAlive) {return;}
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!isAlive) {return;}
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if(value.isPressed)
        {
            myRigidBody.velocity += new Vector2 (0f, jumpAmount);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        
        {               
            myRigidBody.gravityScale = startGravity;
            myAnimator.SetBool("isClimbing", false);
            return;            
        }
            
        Vector2 climbVelocity = new Vector2 (myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity; 
        myRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);                  
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazard")))
        {
            isAlive = false;
            myAnimator.SetTrigger("isDied");
            myRigidBody.velocity = deathKick;
            mySpriteRenderer.color = dieColor;
            myImpulseSource.GenerateImpulse(1);
            FindObjectOfType<GameSession>().ProcessPlayerDeath(); 
        }
        
    }

    void OnFire(InputValue value)
    {
        if(!isAlive) {return;}
        if(value.isPressed)
        {
            myAnimator.SetTrigger("isShooted");
            Instantiate(arrow, bow.position, transform.rotation);
        }
    }

}
