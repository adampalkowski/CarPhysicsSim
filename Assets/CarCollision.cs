using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Handle collision with the obstacle (e.g., reduce health, play a crash sound)
            Debug.Log("Car crashed into obstacle!");
        }
    }
}
