using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckpointManager : MonoBehaviour
{
    private int currentCheckpointIndex = 0;

    public void UpdateCheckpoint(int newCheckpointIndex)
    {
        // Add logic here to ensure checkpoints are passed sequentially if needed
        currentCheckpointIndex = newCheckpointIndex;
        // You may want to add more logic, such as checking if the new index is greater than the current one.
    }

    public int GetCurrentCheckpointIndex()
    {
        return currentCheckpointIndex;
    }
}
