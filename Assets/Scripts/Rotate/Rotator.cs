using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Speed of rotation
    public float rotationSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}