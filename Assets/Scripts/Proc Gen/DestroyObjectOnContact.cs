using UnityEngine;

public class DestroyObjectOnContact : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }
}
