using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class WinConditionChecker : Singleton<WinConditionChecker>
{
    private Dictionary<PieceType, Vector2Int> _piecePositionDictionary = new Dictionary<PieceType, Vector2Int>();

    private void UpdateDictionary()
    {
        PieceController controller = PieceController.Instance;
        _piecePositionDictionary[PieceType.BottomLeft] = BoardManager.Instance.
            GetCellPosition(controller.BottomLeftPiece.position);
        _piecePositionDictionary[PieceType.BottomRight] = BoardManager.Instance.
            GetCellPosition(controller.BottomRightPiece.position);
        _piecePositionDictionary[PieceType.TopLeft] = BoardManager.Instance.
            GetCellPosition(controller.TopLeftPiece.position);
        _piecePositionDictionary[PieceType.TopRight] = BoardManager.Instance.
            GetCellPosition(controller.TopRightPiece.position);
    }


    public bool IsWin()
    {
        UpdateDictionary();
        int cellSize = BoardManager.Instance.CellSize;
        Vector2Int bottomLeftPiecePosition = _piecePositionDictionary[PieceType.BottomLeft];
        return bottomLeftPiecePosition == _piecePositionDictionary[PieceType.BottomRight] - new Vector2Int(cellSize, 0)
            && bottomLeftPiecePosition == _piecePositionDictionary[PieceType.TopLeft] - new Vector2Int(0, cellSize)
            && bottomLeftPiecePosition == _piecePositionDictionary[PieceType.TopRight] - new Vector2Int(cellSize, cellSize);

    }





}
