using System.Collections.Generic;
using System.Threading;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    [SerializeField] private float _boardWidth = 4;
    [SerializeField] private float _boardHeight = 4;
    [SerializeField] private int _cellSize = 1;
    public int CellSize => _cellSize;
    [SerializeField] private Transform _blockCellsContainer;
    [SerializeField] private Transform _piecesContailer;
    private HashSet<Vector2Int> _blockCells = new HashSet<Vector2Int>();

    void Start()
    {
        GetBlockCellsPosition();
    }

    private void GetBlockCellsPosition()
    {
        foreach (Transform cell in _blockCellsContainer)
        {
            _blockCells.Add(new Vector2Int((int)cell.position.x, (int)cell.position.y));
        }
        foreach (Transform cell in _piecesContailer)
        {
            _blockCells.Add(new Vector2Int((int)cell.position.x, (int)cell.position.y));
        }
    }
    public bool CanMove(Vector3 worldPosition)
    {
        Vector2Int cellPosition = GetCellPosition(worldPosition);
        return IsInBounds(cellPosition) && !IsBlockCell(cellPosition);
    }

    private bool IsBlockCell(Vector2Int cellPosition)
    {
        return _blockCells.Contains(cellPosition);
    }

    private bool IsInBounds(Vector2Int cellPosition)
    {
        return cellPosition.x >= 0 && cellPosition.x < _boardWidth && cellPosition.y >= 0 && cellPosition.y < _boardHeight;
    }

    public Vector2Int GetCellPosition(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.RoundToInt(worldPosition.x / _cellSize), Mathf.RoundToInt(worldPosition.y / _cellSize));
    }


    public void UpdateBlockCellsPosition(Vector3 previousPosition, Vector3 currentPosition)
    {
        _blockCells.Remove(GetCellPosition(previousPosition));
        _blockCells.Add(GetCellPosition(currentPosition));
    }


}
