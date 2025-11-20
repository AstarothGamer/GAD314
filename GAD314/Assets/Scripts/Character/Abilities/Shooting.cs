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

    public int ammoCage = 30;
    public int ammoReserve = 60;

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
                Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                AudioManager.Instance.PlaySoundAtPoint("gun-fire", firePoint.transform.position, 2f);
                ammoCage -= 1;
                timer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ammoCage < 30)
            {
                //play annimation of reload
                //play sound of reload

                StartCoroutine(Reload());
            }
        }

        ammoText.text = ammoCage + "/" + ammoReserve;
    }
    
    public IEnumerator Reload()
    {
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
    }
}
