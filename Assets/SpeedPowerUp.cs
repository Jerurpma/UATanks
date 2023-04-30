using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    // Define the powerup behavior here. This example increases the object's speed.
    public float speedBoost = 5f;
    public GameObject prefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has a Rigidbody2D component attached to it.
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Activate the powerup.
            rb.velocity *= speedBoost;

            // Destroy the powerup object.
            Destroy(prefab);
        }
    }
}
