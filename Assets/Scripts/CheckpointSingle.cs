using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
  // OnTriggerEnter2D is called when another collider enters the trigger zone
      void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);

        if (other.CompareTag("Car"))
        {
            // Find the index of this checkpoint in the scene hierarchy
            int checkpointIndex = transform.GetSiblingIndex();

            // Log the name of the car GameObject for further inspection

            CheckpointManager checkpointManager = other.GetComponent<CheckpointManager>();

            if (checkpointManager != null)
            {
                checkpointManager.UpdateCheckpoint(checkpointIndex);
            }
            else
            {
                Debug.LogError("CheckpointManager not found on the car GameObject.");
            }
        }
    }
}
