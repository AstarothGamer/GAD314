using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] GrapplingGun grapple;
    [SerializeField] PlayerMovement grappleMov;
    [SerializeField] PlayerMovement2 basicMov;
    [SerializeField] private PlayerDataSO playerSO;
    int i = 0;

    void Start()
    {
        transform.position = playerSO.playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(i < 10)
        {
            i += 1;
            transform.position = playerSO.playerPosition;
        }
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
