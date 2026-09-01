using UnityEngine;

public class Apple : PickUp
{
    [SerializeField] float adjustmentSpeed = 3f;
    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindAnyObjectByType<LevelGenerator>();
    }

    protected override void onPickUp()
    {
        levelGenerator.ChangeChunkMoveSpeed(adjustmentSpeed);
    }
}
