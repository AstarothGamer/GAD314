using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGrapple : MonoBehaviour
{
    // Reference to the GrapplingGun script to check if grappling is active
    public GrapplingGun grappling;

    // The target rotation we want to smoothly transition to
    private Quaternion desiredRotation;

    // The speed at which the rotation happens
    private float rotationSpeed = 5f;

    void Update()
    {
        // If the player is not grappling, maintain the current rotation based on the parent object
        if (!grappling.IsGrappling())
        {
            // Set desired rotation to the parent's rotation (no grappling)
            desiredRotation = transform.parent.rotation;
        }
        else
        {
            // Calculate the direction to the grapple point and get the corresponding rotation
            desiredRotation = Quaternion.LookRotation(grappling.GetGrapplePoint() - transform.position);
        }

        // Smoothly interpolate between the current rotation and the desired rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
