using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeightTransfer : MonoBehaviour
{
    [Header("Visual Representation")]
    public SpriteRenderer centerOfMassRenderer;  // Assign this in the Unity Editor

    // Car parameters
    public float wheelbase = 3.0f;
    public float height = 20.0f;
    public float mass = 1000.0f;
    float CG_X = 0.0f ;
    float CG_Y = 0;
    // Car state
    public float speed;
    public Transform carTransform;  // Reference to the car's transform

    // Start is called before the first frame update
    void Start()
    {
           if (carTransform == null)
        {
            // If carTransform is not assigned, use the transform of the GameObject this script is attached to
            carTransform = transform;
        }
                centerOfMassRenderer.transform.position = carTransform.position;

    }

    // Update is called once per frame
    public void UpdateWeightTransfer(float currentSpeed,float acceleration,float carDirection)
{
    speed = currentSpeed;  // Update the car's speed

        CalculateWeightTransfer(acceleration,carDirection);
        UpdateCenterOfMassPosition(acceleration);
    }
    
 void CalculateWeightTransfer(float acceleration,float carDirection)
    {
        Debug.Log("carDirection:"+carDirection);
    float MASS_VALUE = 0;
    // If accelerating, move the center of mass towards the back
    if (acceleration > 0)
    {
        MASS_VALUE = -(height / wheelbase) * mass * acceleration;
    }
    // If decelerating, move the center of mass towards the front
    else if (acceleration < 0)
    {
    MASS_VALUE = (height / wheelbase) * mass * acceleration;
    }
              CG_X = Mathf.Sin(carDirection * Mathf.Deg2Rad) * MASS_VALUE;
              CG_Y = Mathf.Cos(carDirection * Mathf.Deg2Rad) * MASS_VALUE;
    }

    void UpdateCenterOfMassPosition( float acceleration)
    {
       
        Vector2 carPosition = carTransform.position;

        // Adjust the position based on the sign of acceleration
        float offsetMultiplier = (acceleration > 0) ? 1.0f : -1.0f;
        Vector2 centerOfMassPosition = new Vector2(carPosition.x + CG_X* offsetMultiplier, carPosition.y + CG_Y * offsetMultiplier);

        centerOfMassRenderer.transform.position = centerOfMassPosition;
    }
}
