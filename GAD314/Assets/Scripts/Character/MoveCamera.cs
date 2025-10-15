using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Reference to the player's Transform component
    public Transform player;

    void Update()
    {
        // Update the camera's position to match the player's position every frame
        transform.position = player.transform.position;
    }
}
