using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public enum PieceType
{
    BottomLeft, BottomRight, TopLeft, TopRight
}
public class PieceController : Singleton<PieceController>
{
    [Header("Orange Pieces Reference")]
    [SerializeField] private Transform _bottomLeftPiece;
    public Transform BottomLeftPiece => _bottomLeftPiece;

    [SerializeField] private Transform _bottomRightPiece;
    public Transform BottomRightPiece => _bottomRightPiece;

    [SerializeField] private Transform _topLeftPiece;
    public Transform TopLeftPiece => _topLeftPiece;
    [SerializeField] private Transform _topRightPiece;
    public Transform TopRightPiece => _topRightPiece;
    [Header("Swipe Detector Reference")]
    [SerializeField] private SwipeDetector _detector;

    [Header("Movement Config")]
    [SerializeField] private PieceMovementConfig _movementConfig;

    private List<Transform> _orangePieces = new List<Transform>();
    private bool _disableControl = false;

    void Start()
    {
        InitializePiecesList();
    }
    
    void Update()
    {
        HandlePlayerInput();
    }

    private void InitializePiecesList()
    {
        _orangePieces.Add(_bottomLeftPiece);
        _orangePieces.Add(_bottomRightPiece);
        _orangePieces.Add(_topLeftPiece);
        _orangePieces.Add(_topRightPiece);
    }
    private Tween MovePiece(Transform pieceTransform, Vector3 movement)
    {
        Vector3 currentPosition = pieceTransform.position;
        Vector3 newPosition = pieceTransform.position + movement;
        if (BoardManager.Instance.CanMove(newPosition))
        {
            BoardManager.Instance.UpdateBlockCellsPosition(currentPosition, newPosition);
            return _movementConfig.Move(pieceTransform, newPosition, () => { });
        }
        return null;
    }

    private IEnumerator MoveAllPiece(Vector3 movement)
    {
        Sequence sequence = DOTween.Sequence();
        foreach (var piece in _orangePieces)
        {
            var moveTween = MovePiece(piece, movement);
            if (moveTween != null)
            {
                sequence.Join(moveTween);
            }
        }
        yield return sequence.Play().WaitForCompletion();
        yield return new WaitForEndOfFrame();
        _disableControl = false;
        if (WinConditionChecker.Instance.IsWin())
        {
            _disableControl = true;
            float delay = 1f;
            yield return new WaitForSeconds(delay);
            MessageManager.SendMessage(new Message(GameMessageType.OnGameWin));
        }
    }

    private void HandlePlayerInput()
    {
        if (_disableControl) return;
        if (_detector.SwipeUp)
        {
            Debug.Log("MOVE UP");
            MoveUp();
            _detector.ResetFlags();
            _disableControl = true;
        }
        else if (_detector.SwipeLeft)
        {
            MoveLeft();
            _detector.ResetFlags();
            _disableControl = true;
        }
        else if (_detector.SwipeRight)
        {
            MoveRight();
            _detector.ResetFlags();
            _disableControl = true;
        }
        else if (_detector.SwipeDown)
        {
            MoveDown();
            _detector.ResetFlags();
            _disableControl = true;
        }
    }

    private void MoveUp()
    {
        _orangePieces.Sort((a, b) => b.position.y.CompareTo(a.position.y));
        StartCoroutine(MoveAllPiece(Vector3.up));
    }

    private void MoveDown()
    {
        _orangePieces.Sort((a, b) => a.position.y.CompareTo(b.position.y));
        StartCoroutine(MoveAllPiece(Vector3.down));

    }
    private void MoveLeft()
    {
        _orangePieces.Sort((a, b) => a.position.x.CompareTo(b.position.x));
        StartCoroutine(MoveAllPiece(Vector3.left));
    }

    private void MoveRight()
    {
        _orangePieces.Sort((a, b) => b.position.x.CompareTo(a.position.x));
        StartCoroutine(MoveAllPiece(Vector3.right));
    }



}
