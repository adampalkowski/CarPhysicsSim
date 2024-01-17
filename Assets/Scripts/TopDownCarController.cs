using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car settings")]
     float velocityVsUp = 0;

    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 6.0f;
    public float maxSpeed = 20;
    public float drag = 0.1f;
    public float rollingResistance = 0.6f;
    public float brakingFactor = 0.6f;
    public float mass = 1000.0f;

    public float carDirection = 0.0f;
    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;
    WeightTransfer weightTransfer;
    Rigidbody2D carRigidbody2D;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
                        weightTransfer = GetComponent<WeightTransfer>();  // Add this line
        carRigidbody2D.mass = 1.0f;

        
    }

    void FixedUpdate()
    {           
        ApplySteering();
        ApplyEngineForce();
        KillOrthogonalVelocity();
       weightTransfer.UpdateWeightTransfer(carRigidbody2D.velocity.magnitude, accelerationInput,carDirection);

    }

    void ApplyEngineForce()
    {
            
        
      float speed = carRigidbody2D.velocity.magnitude;
   
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


     //Caculate how much "forward" we are going in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        //Limit so we cannot go faster than the 50% of max speed in the "reverse" direction
        if (velocityVsUp < -maxSpeed*0.5f && accelerationInput < 0)
            return;

        //Limit so we cannot go faster in any direction while accelerating
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;
        
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        carRigidbody2D.AddForce(engineForceVector);
     
    }

void ApplySteering()
{

  
    rotationAngle -= steeringInput * turnFactor ;
    carRigidbody2D.MoveRotation(rotationAngle);
    carDirection = 360-(rotationAngle + 360) % 360;

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

    float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity, out bool isBraking)
    {
        
         lateralVelocity = GetLateralVelocity();
        isBraking = false;
        if(accelerationInput<0 && velocityVsUp>0){
            isBraking = true;
            return true;
        }else{
        }
         if (Mathf.Abs(GetLateralVelocity()) > 4.0f)
         {
            return true;
         }
        return false;   
    }  


}
