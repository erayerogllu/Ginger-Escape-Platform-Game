using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int playerCoinNumber = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerCoinNumber++;
            Destroy(gameObject);
            Debug.Log(playerCoinNumber);
        }

        
    }

}
