using TMPro;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject pickUpPanel;
    [SerializeField] private TMP_Text pickUpText;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] Camera cam;
    [SerializeField] private bool inArea = false;
    RaycastHit hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(inArea)
            {
                PickUpItem();
            }
        }
        
    }

    void FixedUpdate()
    {
        CheckingRaycast();
    }

    private void CheckingRaycast()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        

        if (Physics.Raycast(ray, out hit, 7f))
        {
            if (hit.collider.CompareTag("Gun"))
            {
                pickUpPanel.SetActive(true);
                pickUpText.text = "Press E to pick up the gun";
                inArea = true;
            }
            else if (hit.collider.CompareTag("Katana"))
            {
                pickUpPanel.SetActive(true);
                pickUpText.text = "Press E to pick up the katana";
                inArea = true;
            }
            else if (hit.collider.CompareTag("GrapplingGun"))
            {
                pickUpPanel.SetActive(true);
                pickUpText.text = "Press E to pick up the grappling gun";
                inArea = true;
            }
        }
        else
        {
            pickUpPanel.SetActive(false);
            inArea = false;
        }
    }

    void PickUpItem()
    {
        if(hit.collider.GetComponentInParent<Katana>())
        {
            hit.collider.GetComponentInParent<Item>().PickedUp();
            weaponManager.katana = true;
        }
        else if(hit.collider.GetComponentInParent<Gun>())
        {
            hit.collider.GetComponentInParent<Item>().PickedUp();
            weaponManager.gun = true;
        }
        else if(hit.collider.GetComponentInParent<GrapGun>())
        {
            hit.collider.GetComponentInParent<Item>().PickedUp();
            weaponManager.grapplingGun = true;
        }
    }
}
