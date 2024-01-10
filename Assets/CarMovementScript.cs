using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementScript : MonoBehaviour
{

    public float speed = 10f;
    public float rotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
           transform.Translate(Vector2.up * speed * Time.deltaTime);
// Rotate the car based on input
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * -horizontalInput * rotationSpeed * Time.deltaTime);
    
    }
}
