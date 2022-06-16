using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myCapsuleCollider;
    [SerializeField] float arrowVelocityMultiplier;
    PlayerMovement player;
    float arrowVelocity;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();  
        player = FindObjectOfType<PlayerMovement>();
        arrowVelocity = player.transform.localScale.x * arrowVelocityMultiplier;
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2 (arrowVelocity, 0f);
        FlipArrowSprite();
    }
    void FlipArrowSprite()
    {
        bool arrowHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if(arrowHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x)* 0.06f, 0.07f);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
       Destroy(gameObject);
    }
    
}
