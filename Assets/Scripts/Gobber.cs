using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gobber : MonoBehaviour
{
    Rigidbody2D gobberRigidBody;
    [SerializeField] float gobberSpeed;

    void Start()
    {
        gobberRigidBody = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        GobberMovement();
        
        
    }


    void GobberMovement()
    {
        gobberRigidBody.velocity = new Vector2(gobberSpeed, 0);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        gobberSpeed = -gobberSpeed;
        FlippingGobbersFace();
        
    }
    
    void FlippingGobbersFace()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(gobberRigidBody.velocity.x)), 1f);

    }
        
    
}
