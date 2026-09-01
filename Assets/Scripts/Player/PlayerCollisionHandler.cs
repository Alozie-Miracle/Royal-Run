using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{

    [SerializeField] Animator animator;
    const string hitString = "Hit";

    // adding a cool down timer
    private float cooldown = 0.5f;
    private float lastHitTime = 0f;


    private void OnCollisionEnter(Collision other) {
        if (Time.time - lastHitTime > cooldown) {
            animator.SetTrigger(hitString);
            lastHitTime = Time.time;
        }
    }
}
