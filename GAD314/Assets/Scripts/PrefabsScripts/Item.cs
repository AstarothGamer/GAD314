using UnityEditor;
using UnityEngine;

public class Item : MonoBehaviour
{
    public void PickedUp()
    {
        Destroy(gameObject);
    }
}
