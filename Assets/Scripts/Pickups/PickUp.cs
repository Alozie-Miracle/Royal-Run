using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    const string playerString = "Player";

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(playerString))
        {
            onPickUp();
            Destroy(gameObject);
        }
    }

    protected abstract void onPickUp();
}
