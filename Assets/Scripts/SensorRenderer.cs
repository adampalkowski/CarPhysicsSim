using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorRenderer : MonoBehaviour
{    public float maxDistanceToObstacle = 10f;
    SpriteRenderer sensorFrontRenderer;

     public float sensorLength = 800f; // Adjust the sensor length as needed
    private LineRenderer[] sensorLineRenderers;
  float[] sensorDistances = new float[5]; // Array to store sensor distances
    public LayerMask ObstacleLayer; // Set the obstacle layer in the Unity Inspector
void AdjustSensorFrontLength(SpriteRenderer sensorFrontRenderer, float newLength)
{
    Vector3 scale = sensorFrontRenderer.transform.localScale;
    float originalLength = scale.y;

    // Calculate the difference in length
    float lengthDifference = newLength - originalLength;

    // Adjust the local scale to change the length from the bottom
    scale.y = newLength;
    sensorFrontRenderer.transform.localScale = scale;

    // Move the sensor down by half of the length difference
    sensorFrontRenderer.transform.Translate(0f,lengthDifference * 0.5f, 0f, Space.Self);

}

void AdjustSensorRightLength(SpriteRenderer sensorRightRenderer, float newLength)
{
    Vector3 scale = sensorRightRenderer.transform.localScale;
    float originalLength = scale.x;  // Use x-axis for horizontal scaling

    // Calculate the difference in length
    float lengthDifference = newLength - originalLength;

    // Adjust the local scale to change the length from the right side
    scale.x = newLength;
     scale.y = 0.1f;
    sensorRightRenderer.transform.localScale = scale;

    // Move the sensor right by half of the length difference
    sensorRightRenderer.transform.Translate(lengthDifference * 0.5f, 0f, 0f, Space.Self);
}


void AdjustSensorLeftLength(SpriteRenderer sensorLeftRenderer, float newLength)
{
    Vector3 scale = sensorLeftRenderer.transform.localScale;
    float originalLength = scale.x;  // Use x-axis for horizontal scaling

    // Calculate the difference in length
    float lengthDifference = newLength - originalLength;

    // Adjust the local scale to change the length from the left side
    scale.x = newLength;
         scale.y = 0.1f;
    sensorLeftRenderer.transform.localScale = scale;

    // Move the sensor left by half of the length difference
    sensorLeftRenderer.transform.Translate(-lengthDifference * 0.5f, 0f, 0f, Space.Self);
}

    public void UpdateSensors() {  
            // Front sensor
        sensorDistances[0] = CastRay(transform.up);
     
        // Left sensor
        sensorDistances[1] = CastRay(-transform.right);

        // Right sensor
        sensorDistances[2] = CastRay(transform.right);

        // 45 degrees left sensor
        sensorDistances[3] = CastRay(transform.up + transform.right);

        // 45 degrees right sensor
        sensorDistances[4] = CastRay(transform.up - transform.right);
    // Search for the SensorRenderer GameObject as a child of the Car
        Transform sensorRendererTransform = transform.Find("SensorRenderer");

        if (sensorRendererTransform != null)
        {
            // Within SensorRenderer, search for the SensorFront GameObject and get the SpriteRenderer component
            Transform sensorFrontTransform = sensorRendererTransform.Find("SensorFront");
            Transform sensorLeftTransform= sensorRendererTransform.Find("SensorLeft");
            Transform sensorRightTransform= sensorRendererTransform.Find("SensorRight");    
            Transform sensorFrontRightTransform= sensorRendererTransform.Find("SensorFrontRight");
            Transform sensorFrontLeftTransform= sensorRendererTransform.Find("SensorFrontLeft");

            if (sensorFrontTransform != null)
            {
                SpriteRenderer sensorFrontRenderer = sensorFrontTransform.GetComponent<SpriteRenderer>();
                if (sensorFrontRenderer != null)
                {
                    // Adjust the length of the sensor based on the distance to the obstacle
                    AdjustSensorFrontLength(sensorFrontRenderer, sensorDistances[0]);
                }
                else
                {
                    Debug.LogError("SensorFront SpriteRenderer not found under SensorRenderer.");
                }
            }
            else
            {
                Debug.LogError("SensorFront GameObject not found under SensorRenderer.");
            }

            if (sensorLeftTransform != null)
            {
                SpriteRenderer sensorLeftRenderer = sensorLeftTransform.GetComponent<SpriteRenderer>();
                if (sensorLeftRenderer != null)
                {
                    // Adjust the length of the sensor based on the distance to the obstacle
                    AdjustSensorLeftLength(sensorLeftRenderer, sensorDistances[1]);
                }
                else
                {
                    Debug.LogError("SensorLeft SpriteRenderer not found under SensorRenderer.");
                }
            }
            else
            {
                Debug.LogError("SensorLeft GameObject not found under SensorRenderer.");
            }
            
            if (sensorRightTransform != null)
            {
                SpriteRenderer sensorRightRenderer = sensorRightTransform.GetComponent<SpriteRenderer>();
                if (sensorRightRenderer != null)
                {
                    // Adjust the length of the sensor based on the distance to the obstacle
                    AdjustSensorRightLength(sensorRightRenderer, sensorDistances[2]);
                }
                else
                {
                    Debug.LogError("SensorRight SpriteRenderer not found under SensorRenderer.");
                }
            }
            else
            {
                Debug.LogError("SensorRight GameObject not found under SensorRenderer.");
            }

            if (sensorFrontRightTransform != null)
            {
                SpriteRenderer sensorFrontRightRenderer = sensorFrontRightTransform.GetComponent<SpriteRenderer>();
                if (sensorFrontRightRenderer != null)
                {
                    // Adjust the length of the sensor based on the distance to the obstacle
                    AdjustSensorFrontLength(sensorFrontRightRenderer, sensorDistances[3]);
                }
                else
                {
                    Debug.LogError("SensorFrontRight SpriteRenderer not found under SensorRenderer.");
                }
            }
            else
            {
                Debug.LogError("SensorFrontRight GameObject not found under SensorRenderer.");
            }

            if (sensorFrontLeftTransform != null)
            {
                SpriteRenderer sensorFrontLeftRenderer = sensorFrontLeftTransform.GetComponent<SpriteRenderer>();
                if (sensorFrontLeftRenderer != null)
                {
                    // Adjust the length of the sensor based on the distance to the obstacle
                    AdjustSensorFrontLength(sensorFrontLeftRenderer, sensorDistances[4]);
                }
                else
                {
                    Debug.LogError("SensorFrontLeft SpriteRenderer not found under SensorRenderer.");
                }
            }
            else
            {
                Debug.LogError("SensorFrontLeft GameObject not found under SensorRenderer.");
            }
        }
        else
        {
            Debug.LogError("SensorRenderer GameObject not found under Car.");
        }

       
    }

    float CastRay(Vector2 direction)
    {
        Vector2 startPosition = (Vector2)(transform.position);
        RaycastHit2D hit = Physics2D.Raycast( (Vector2)(transform.position), direction, Mathf.Infinity,LayerMask.GetMask("ObstacleLayer"));
        Debug.Log($"Hit distance: {hit.distance}");

          if (hit.collider != null)
    {
        Debug.DrawLine(transform.position, hit.point, Color.red);
        Debug.Log($"Hit distance: {hit.distance}");
        return hit.distance;
    }
    else
    {
        Debug.DrawRay(transform.position, direction * 100f, Color.green);
        Debug.Log("No hit, returning a large distance.");
        return 10f; // Modify this value to suit your needs
    }
    }
}
