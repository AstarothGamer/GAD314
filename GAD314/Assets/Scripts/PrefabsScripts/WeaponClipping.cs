using UnityEngine;

public class WeaponClipping : MonoBehaviour
{
    public Transform weaponHolder;
    public Transform cameraTransform;
    public float checkDistance = 2f;

    public Animator weaponAnimator;    

    [SerializeField] public bool isHidden = false;

    private bool gizmoHit = false; 
    private Vector3 gizmoHitPoint;

    void Start()
    {
        if (weaponHolder == null) weaponHolder = transform;

    }

    void Update()
    {
        

        
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        bool tooClose = Physics.Raycast(
            cameraTransform.position,
            cameraTransform.forward,
            out hit,
            checkDistance
        );

        gizmoHit = tooClose;
        gizmoHitPoint = tooClose ? hit.point : cameraTransform.position + cameraTransform.forward * checkDistance;


        if (tooClose && !isHidden)
        {
            isHidden = true;
            weaponAnimator.SetTrigger("HideWeapon");   
        }
        else if (!tooClose && isHidden)
        {
            // isHidden = false;
            weaponAnimator.SetTrigger("ShowWeapon");  
        }
    }

    public void OnShowWeaponFinished()
    {
        isHidden = false;   
    }

    private void OnDrawGizmos()
    {
        if (cameraTransform == null) return;

        Gizmos.color = gizmoHit ? Color.red : Color.green;

        Gizmos.DrawLine(
            cameraTransform.position,
            cameraTransform.position + cameraTransform.forward * checkDistance
        );

        Gizmos.DrawSphere(gizmoHitPoint, 0.05f);
    }
}
