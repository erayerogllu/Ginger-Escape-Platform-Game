using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    Rigidbody2D firerigidBody;
    PlayerMovement playerMovementTransfomValues;
    
    float xSpeed;
    float fireTransform;
    [SerializeField] float fireSpeed;

    void Start()
    {
        firerigidBody = GetComponent<Rigidbody2D>();
        playerMovementTransfomValues = FindObjectOfType<PlayerMovement>();
        xSpeed = playerMovementTransfomValues.transform.localScale.x * fireSpeed;
        fireTransform = (playerMovementTransfomValues.transform.localScale.x) * (-1);
  
    }

    
    void Update()
    {
        FireMovement();
        
    }

    void FireMovement()
    {
        firerigidBody.velocity = new Vector2(xSpeed, 0f);
        transform.localScale = new Vector2(fireTransform, transform.localScale.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemies")
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
