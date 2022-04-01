using UnityEngine;

public class GridObject
{
    private GameObject _gameObject;

    private readonly IGrid _grid;
    private readonly GridIndex _gridIndex;

    public GridIndex GridIndex { get => _gridIndex; }

    public GridObject(IGrid grid, GridIndex gridIndex)
    {
        _grid = grid;
        _gridIndex = gridIndex;
    }

    public void SetObject(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

    public bool IsEmpty() { return _gameObject == null; }

    public override string ToString()
    {
        return _gridIndex.X + ", " + _gridIndex.Y;
    }
}