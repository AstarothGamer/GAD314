using System.Collections;
using TMPro;
using UnityEngine;

public class GrappleDoor : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject doorPanel;
    [SerializeField] private TMP_Text doorText;

    private bool inArea = false;
    private bool isOpened = false;
    private bool isOpening = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inArea)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();
            }
        }
    }

    public void OpenDoor()
    {
        if (isOpened || isOpening) return;

        isOpening = true;
        inArea = false;

        if (doorPanel != null)
            doorPanel.SetActive(false);

        StartCoroutine(OpenDoorRoutine());
    }

    private IEnumerator OpenDoorRoutine()
    {
        Quaternion startRot = transform.localRotation;
        Quaternion endRot = Quaternion.Euler(0f, -90f, 0f);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / 1f;
            transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }

        transform.localRotation = endRot;
        isOpened = true;
        isOpening = false;
        
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!isOpened)
            {
                doorPanel.SetActive(true);

                if(!other.GetComponent<Inventory>().key)
                {
                    doorText.text = "You don't have the key!";
                }
                else
                {
                    inArea = true;
                    doorText.text = "You found the key. Press E to open the door.";
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        doorPanel.SetActive(false);

        inArea = false;
    }
}
