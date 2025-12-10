using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    [SerializeField] private GameObject key;

    public bool dropped = false;

    public void DropKey()
    {
        if (dropped) return;   
        dropped = true;

        Instantiate(key, transform.position, Quaternion.identity);
    }
}
