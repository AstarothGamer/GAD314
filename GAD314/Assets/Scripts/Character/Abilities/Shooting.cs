using System.Collections;
using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private PlayerDataSO playerSO;
    [SerializeField] private WeaponClipping clipFix;
    [SerializeField] private Camera playerCamera;

    public int ammoCage = 30;
    public int ammoReserve = 60;
    private bool reloading = false;

    private float timer = 0.2f;
    // [SerializeField] 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        ammoCage = playerSO.ammoCage;
        ammoReserve = playerSO.ammoReserve;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(!clipFix.isHidden)
            {
                if(ammoCage < 1)
                {
                    //play sound of empty cage
                    return;
                }
                if (timer < 0.2f) return;
                Shoot();
                timer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ammoCage < 30 && !reloading)
            {
                //play annimation of reload
                //play sound of reload

                StartCoroutine(Reload());
            }
        }

        ammoText.text = ammoCage + "/" + ammoReserve;
    }

    private void Shoot()
    {
        ammoCage--;

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000f);
        }

        Vector3 dir = (targetPoint - firePoint.transform.position).normalized;

        Quaternion rot = Quaternion.LookRotation(dir);

        Instantiate(bulletPrefab, firePoint.transform.position, rot);
        AudioManager.Instance.PlaySoundAtPoint("gun-fire", firePoint.transform.position, 2f);
    }
    
    public IEnumerator Reload()
    {
        reloading = true;
        int i = 30 - ammoCage;
        int r = ammoCage;
        ammoCage = 0;
        AudioManager.Instance.PlaySoundAtPoint("Gun_Reload", transform.position);
        yield return new WaitForSecondsRealtime(1f);

        
        
        if (ammoReserve < i)
        {
            i = ammoReserve;
        }
        
        ammoCage = r + i;
        ammoReserve -= i;
        reloading = false;
    }

    public void GettingAmmo(int amount)
    {
        ammoReserve += amount;

        if(ammoReserve > 360)
        {
            ammoReserve = 360;
        }
    }
}
