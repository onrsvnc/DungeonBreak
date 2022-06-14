using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobberMovement : MonoBehaviour
{
    [SerializeField] float moveVelocity = 1f;
    Rigidbody2D myRigidBody;
    BoxCollider2D myBoxCollider;



    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        myRigidBody.velocity = new Vector2 (moveVelocity, 0);
    }
    void OnTriggerExit2D(Collider2D myBoxCollider)
    {  
        moveVelocity = -moveVelocity; 
        FlipSprite();
    }
    
    void FlipSprite()
    {
        transform.localScale = new Vector2 (Mathf.Sign(moveVelocity), 1f);
    }
}

