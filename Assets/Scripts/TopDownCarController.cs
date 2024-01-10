using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;
    public float drag = 0.1f;
    public float rollingResistance = 0.6f;
    public float brakingFactor = 0.6f;

    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    Rigidbody2D carRigidbody2D;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {           
        ApplySteering();
        ApplyEngineForce();
        KillOrthogonalVelocity();
   
    }

    void ApplyEngineForce()
    {
            
        
      float speed = carRigidbody2D.velocity.magnitude;
      Debug.Log("SPEED:"+speed);

    float brakingForce = brakingFactor * speed;

    Vector2 brakingForceVector = -carRigidbody2D.velocity.normalized * brakingForce;
      if(Input.GetKey(KeyCode.Space)){
            if(speed>0){
                carRigidbody2D.AddForce(brakingForceVector);
            }
     }
      Vector2 dragForce = -drag * carRigidbody2D.velocity.normalized * speed;
      Vector2 rollingResistanceForce = -rollingResistance * carRigidbody2D.velocity.normalized ;

        carRigidbody2D.AddForce(dragForce); 
    
        carRigidbody2D.AddForce(rollingResistanceForce); 

        
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        carRigidbody2D.AddForce(engineForceVector);
     
    }

void ApplySteering()
{

  
    rotationAngle -= steeringInput * turnFactor ;
    carRigidbody2D.MoveRotation(rotationAngle);
}

    void KillOrthogonalVelocity()
    { 

        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);
            
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }



    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

}
