using UnityEngine;

public class Apple : PickUp
{
    [SerializeField] float adjustmentSpeed = 3f;
    LevelGenerator levelGenerator;

    // private void Start() {
    //     levelGenerator = FindAnyObjectByType<LevelGenerator>();
    // }
    public void Init(LevelGenerator levelGenerator)
    {
        this.levelGenerator = levelGenerator;
    }

    protected override void onPickUp()
    {
        levelGenerator.ChangeChunkMoveSpeed(adjustmentSpeed);
    }
}
