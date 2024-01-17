using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnvironment : MonoBehaviour
{   
        TopDownCarController topDownCarController;

    // Start is called before the first frame update
    void Start()
    {
     topDownCarController = GetComponent<TopDownCarController>(); 
    }
    void Reset(){
         // Reset car position, rotation, and any other relevant variables
        // (e.g., respawn the car on the track)


        // Additional reset logic if needed
  
        topDownCarController.rotationAngle = 0;
        transform.position =new  Vector3(-25f,-8f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        topDownCarController.carRigidbody2D.velocity = Vector2.zero;    
        topDownCarController.collisionCheck=false;
    }
     // Update is called once per frame
    void Update()
    {
        if(topDownCarController.collisionCheck){
            Reset();
        }
        // Detect 'R' key press to reset the environment
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }



    // Take an action in the environment
    public void TakeAction(int action)
    {
       
       Vector2 inputVector = Vector2.zero;
         switch (action){
            case 0:
                inputVector.x = 0;
                inputVector.y = 1;
            break;
            case 1:
                inputVector.x = 0;
                inputVector.y = -1;
            break;
            case 2:
                inputVector.x = -1;
                inputVector.y = 0;
            break;
            case 3: 
                inputVector.x = 1;
                inputVector.y = 0;
            break;
            
         }   

        topDownCarController.SetInputVector(inputVector);

    }
/*
    // Get the current state of the environment
    public int GetState()
    {
        int position = transform.position;
        int speed = topDownCarController.carRigidbody2D.velocity;
        int orientation = transform.rotation;
        int distanceToCheckpoint = Vector2.Distance(position, checkpoints[progress].position);
        int idOfCheckpoint = checkpoints[progress].id;
        int acceleration = topDownCarController.accelerationInput;
        int lastAction  = topDownCarController.lastAction;
        int progress = topDownCarController.progress;
        int collision = topDownCarController.collision ? 1 : 0;  


    int stateIdentifier = CombineHashCodes(
        position.GetHashCode(),
        speed.GetHashCode(),
        orientation.GetHashCode(),
        distanceToCheckpoint.GetHashCode(),
        idOfCheckpoint.GetHashCode(),
        acceleration.GetHashCode(),
        lastAction.GetHashCode(),
        progress.GetHashCode(),
        collision.GetHashCode()
    );

    return stateIdentifier;
    }
    private int CombineHashCodes(params int[] hashCodes)
    {
        unchecked
        {
            int hash = 17;
            foreach (int hashCode in hashCodes)
            {
                hash = hash * 31 + hashCode;
            }
            return hash;
        }
    }
*/
    // Get the number of possible actions in the environment
    public int GetNumActions()
    {
        // Return the number of possible actions (e.g., left, right, accelerate, brake)
        return 4;  // Replace with the actual number of actions
    }

    // Get the number of possible states in the environment
    public int GetNumStates()
    {
        // Return the number of possible states based on your state representation
        return 9;
    }

    // Example: Check if the episode is done
    public bool IsDone()
    {
        // Implement logic to determine if the episode is done (e.g., car collision)
        return topDownCarController.collisionCheck ;  // Replace with the actual condition
    }
}
