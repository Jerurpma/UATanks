using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Debug.Log("Power up Picked up");
    }
}

