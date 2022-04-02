using UnityEngine;
using System.Collections.Generic;

public class BrickGrid : GridXY
{
    private List<Brick> _bricks;

    [SerializeField] private BrickSpawner _brickSpawner;

    private void Start()
    {
        _bricks = new List<Brick>();
        _brickSpawner.OnBrickSpawned += BrickSpawner_OnBrickSpawned;
    }

    private void BrickSpawner_OnBrickSpawned(Brick brick)
    {
        _bricks.Add(brick);
    }

    public void MoveBricksDown()
    {
        foreach (Brick brick in _bricks)
        {
            GridIndex brickCurrentGridIndex = GetGridIndex(brick.transform.position);
            brickCurrentGridIndex.Y -= 1;

            Vector3 brickNewPosition = GetGridObjectCenter(brickCurrentGridIndex);

            brick.transform.position = brickNewPosition;
        }
    }
}
