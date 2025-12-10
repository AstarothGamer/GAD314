using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private float swingAngle = -60f;
    [SerializeField] private float swingDuration = 0.25f;
    [SerializeField] private bool returnToIdle = true;
    [SerializeField] private AnimationCurve swingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [SerializeField] public float damage = 25f;
    // [SerializeField] private float reach = 2f;
    // [SerializeField] private float hitRadius = 2f;
    // [SerializeField] private LayerMask hitMask;
    [SerializeField] private float attackCooldown = 0.4f;


    public bool isSwinging = false;
    private float lastAttackTime = -99f;
    private Quaternion weaponIdleRotation;

    private Camera cam;

    public bool used = false;

    void Start()
    {
        
        cam = GetComponent<Camera>();
        if (weaponTransform != null)
            weaponIdleRotation = weaponTransform.localRotation;

        if (swingCurve == null || swingCurve.length == 0)
        {
            swingCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (isSwinging) return;
        if (Time.time < lastAttackTime + attackCooldown) return;

        lastAttackTime = Time.time;
        StartCoroutine(PerformSwing());
    }

    private IEnumerator PerformSwing()
    {
        isSwinging = true;
        used = true;

        float halfDuration = swingDuration * 0.5f;
        float t = 0f;

        AudioManager.Instance.PlaySoundAtPoint("Sword_Slash", transform.position);

        while (t < halfDuration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / halfDuration);
            float curveVal = swingCurve.Evaluate(normalized);
            ApplyWeaponRotation(Mathf.Lerp(0f, -swingAngle, curveVal));
            yield return null;
        }

        t = 0f;
        while (t < halfDuration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / halfDuration);
            float curveVal = swingCurve.Evaluate(normalized);
            ApplyWeaponRotation(Mathf.Lerp(-swingAngle, 0f, curveVal));
            yield return null;
        }

        if (weaponTransform != null && returnToIdle)
            weaponTransform.localRotation = weaponIdleRotation;

        yield return null;

        isSwinging = false;
    }

    private void ApplyWeaponRotation(float xAngle)
    {
        if (weaponTransform == null) return;
        Quaternion rot = Quaternion.Euler(xAngle, weaponIdleRotation.eulerAngles.y, weaponIdleRotation.eulerAngles.z);
        weaponTransform.localRotation = rot;
    }
}