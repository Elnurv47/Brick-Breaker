using Utils;
using UnityEngine;
using System.Collections.Generic;

public class GridXY : MonoBehaviour, IGrid
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector3 _origin;

    private GridObject[,] _gridArray;

    private TextMesh[,] _gridDebugArray;

    public int Width { get => _width; }
    public int Height { get => _height; }

    private void Awake()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _gridArray = new GridObject[_width, _height];
        _gridDebugArray = new TextMesh[_width, _height];

        #region Debugging
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                GridIndex gridIndex = new GridIndex(x, y, 0);
                SetGridObject(gridIndex, new GridObject(this, gridIndex));

                _gridDebugArray[x, y] = Utility.CreateWorldText(
                    _gridArray[x, y].ToString(),
                    position: GetGridObjectPosition(new GridIndex(x, y, 0)) + new Vector3(0.5f, 0.5f, 0f) * _cellSize,
                    color: Color.white,
                    fontSize: 5
                );

                Debug.DrawLine(GetGridObjectPositionDebug(new GridIndex(x, y, 0)), GetGridObjectPositionDebug(new GridIndex(x, y + 1, 0)), Color.white, 1000f);
                Debug.DrawLine(GetGridObjectPositionDebug(new GridIndex(x, y, 0)), GetGridObjectPositionDebug(new GridIndex(x + 1, y, 0)), Color.white, 1000f);
            }
        }

        Debug.DrawLine(GetGridObjectPositionDebug(new GridIndex(0, _height, 0)), GetGridObjectPositionDebug(new GridIndex(_width, _height, 0)), Color.white, 1000f);
        Debug.DrawLine(GetGridObjectPositionDebug(new GridIndex(_width, 0, 0)), GetGridObjectPositionDebug(new GridIndex(_width, _height, 0)), Color.white, 1000f);
        #endregion
    }

    private Vector3 GetGridObjectPositionDebug(GridIndex gridIndex)
    {
        return gridIndex.ToVector3() * _cellSize + _origin;
    }

    public Vector3 GetGridObjectPosition(GridIndex gridIndex)
    {
        if (!IsValidCoordinate(gridIndex))
        {
            GridIndex clampedGridIndex = ClampToGrid(gridIndex);
            return GetGridObjectPosition(clampedGridIndex);
        }

        return gridIndex.ToVector3() * _cellSize + _origin;
    }

    private GridIndex ClampToGrid(GridIndex gridIndex)
    {
        gridIndex.X = (gridIndex.X < 0) ? 0 : gridIndex.X;
        gridIndex.X = (gridIndex.X >= _width) ? _width - 1 : gridIndex.X;
        gridIndex.Y = (gridIndex.Y < 0) ? 0 : gridIndex.Y;
        gridIndex.Y = (gridIndex.Y >= _height) ? _height - 1 : gridIndex.Y;

        return gridIndex;
    }

    public Vector3 GetGridObjectCenter(Vector3 cellWorldPosition)
    {
        return GetGridObjectCenter(GetGridIndex(cellWorldPosition));
    }

    public Vector3 GetGridObjectCenter(GridIndex gridIndex)
    {
        if (!IsValidCoordinate(gridIndex))
        {
            gridIndex = ClampToGrid(gridIndex);
        }

        Vector3 offset = new Vector3(1, 1, 0) * _cellSize * 0.5f;
        return gridIndex.ToVector3() * _cellSize + offset + _origin;
    }

    public void SetGridObject(Vector3 cellWorldPosition, GridObject gridObject)
    {
        GridIndex point = GetGridIndex(cellWorldPosition);
        SetGridObject(point, gridObject);
    }

    private void SetGridObject(GridIndex gridIndex, GridObject gridObject)
    {
        if (!IsValidCoordinate(gridIndex)) return;
        _gridArray[gridIndex.X, gridIndex.Y] = gridObject;
    }

    public GridIndex GetGridIndex(Vector3 cellWorldPosition)
    {
        int x = Mathf.FloorToInt((cellWorldPosition - _origin).x / _cellSize);
        int y = Mathf.FloorToInt((cellWorldPosition - _origin).y / _cellSize);

        GridIndex gridIndex = new GridIndex(x, y, 0);

        return IsValidCoordinate(gridIndex) ? gridIndex : ClampToGrid(gridIndex);
    }

    public GridObject GetGridObject(Vector3 cellWorldPosition)
    {
        GridIndex gridIndex = GetGridIndex(cellWorldPosition);
        return IsValidCoordinate(gridIndex) ? _gridArray[gridIndex.X, gridIndex.Y] : default;
    }

    private bool IsValidCoordinate(GridIndex gridIndex)
    {
        return gridIndex.X >= 0 && gridIndex.Y >= 0 && gridIndex.X < _width && gridIndex.Y < _height;
    }

    public void UpdateVisual()
    {
        for (int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
                Vector3 gridObjectCenter = GetGridObjectCenter(new GridIndex(x, y, 0));
                GridObject gridObject = GetGridObject(gridObjectCenter);
                _gridDebugArray[x, y].text = gridObject.ToString();
            }
        }
    }

    public List<GridObject> GetRowAt(int rowIndex)
    {
        List<GridObject> column = new List<GridObject>();

        for (int col = 0; col < _gridArray.GetLength(0); col++)
        {
            GridObject gridObject = _gridArray[col, rowIndex];
            column.Add(gridObject);
        }

        return column;
    }
}
