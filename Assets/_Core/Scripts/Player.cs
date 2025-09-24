using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Apple apple))
        {
            apple.Collect();
        }
    }
}
