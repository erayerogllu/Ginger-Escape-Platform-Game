using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    CapsuleCollider2D gingerBodyCollider;
    BoxCollider2D gingerFeetCollider;
    Animator gingerAnimator;
    Vector2 moveInput;
    Rigidbody2D gingerRigidBody;
    
    [SerializeField] GameObject fireObject;
    [SerializeField] Transform fireObjectTransform;


    [SerializeField] float runSpeed = 0f;
    [SerializeField] float jumpSpeed = 0f;
    [SerializeField] float climbingSpeed = 0f;
    [SerializeField] Vector2 deathKick = new Vector2(20f, 20f);
    float gingerGravityAtStart;
    bool isAlive = true;

    void Start()
    {
        gingerRigidBody = GetComponent<Rigidbody2D>();
        gingerAnimator = GetComponent<Animator>();
        gingerBodyCollider = GetComponent<CapsuleCollider2D>();
        gingerFeetCollider= GetComponent<BoxCollider2D>();
        gingerGravityAtStart = gingerRigidBody.gravityScale;
        Vector2 firePosition = new Vector2(fireObject.transform.localPosition.x, fireObject.transform.localPosition.y);
        
    }

    
    void Update()
    {
        if(!isAlive) { return; }
        Run();
        FlipeSprite();
        ClimbLadder();
        Die();
    }
 
    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }
    
    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!gingerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) )
        {
            return;
            
        }
        if(value.isPressed)
        {
            gingerRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, gingerRigidBody.velocity.y);
        gingerRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(gingerRigidBody.velocity.x) > Mathf.Epsilon;
        gingerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        
    }

    void FlipeSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(gingerRigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(gingerRigidBody.velocity.x), 1f);
        }
        
    }
    
    void ClimbLadder()
    {
        if(!gingerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            gingerRigidBody.gravityScale = gingerGravityAtStart;
            gingerAnimator.SetBool("isClimbing", false);
            return;

        }
        Vector2 climbVelocity = new Vector2(gingerRigidBody.velocity.x, moveInput.y * climbingSpeed);
        gingerRigidBody.velocity = climbVelocity;
        gingerRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(gingerRigidBody.velocity.y) > Mathf.Epsilon;
        gingerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

   
    void Die()
    {
        if(gingerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Water")))
        {
            isAlive = false;
            gingerAnimator.SetTrigger("Dying");
            gingerRigidBody.velocity = deathKick;
            gingerRigidBody.simulated = false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            
        }


    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }

       Instantiate(fireObject, fireObjectTransform.position, transform.rotation);


    }


}
