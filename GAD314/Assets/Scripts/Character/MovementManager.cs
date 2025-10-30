using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] GrapplingGun grapple;
    [SerializeField] PlayerMovement grappleMov;
    [SerializeField] PlayerMovement2 basicMov;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grapple.usingGrappling == true && basicMov.enabled == true)
        {
            basicMov.enabled = false;
            grappleMov.enabled = true;
        }
        else if((grappleMov.grounded == true || basicMov.isGrounded) && grappleMov.enabled == true)
        {
            grappleMov.enabled = false;
            basicMov.enabled = true;
        }
    }
}
