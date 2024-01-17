using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QLearning : MonoBehaviour
{

    private float alpha = 0.1f;
    private float gamma = 0.96f;
    private float epsilon = 0.9f;
    private int numIters=100;    

    private int[] actions = new int[] { 0, 1, 2, 3 }; // 0: up, 1: down, 2: left, 3: right]

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
