using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] float adjustmentSpeed = -2f;
    const string hitString = "Hit";

    // adding a cool down timer
    private float cooldown = 0.5f;
    private float lastHitTime = 0f;

    LevelGenerator levelGenerator;

    void Start() {
        levelGenerator = FindAnyObjectByType<LevelGenerator>();
    }

    private void OnCollisionEnter(Collision other) {

        if (Time.time - lastHitTime > cooldown) {
            levelGenerator.ChangeChunkMoveSpeed(adjustmentSpeed);
            animator.SetTrigger(hitString);
            lastHitTime = Time.time;
        }

    }
}
