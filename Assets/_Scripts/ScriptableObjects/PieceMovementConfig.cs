using System.Collections;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "PieceAnimationConfig", menuName = "Scriptable Objects/PieceAnimationConfig")]
public class PieceMovementConfig : ScriptableObject
{
    [SerializeField] private float _moveTime = 1f;
    [SerializeField] private Ease _ease = Ease.Linear;
    public Ease Ease => _ease;

    public Tween Move(Transform pieceTransform, Vector3 position, TweenCallback callback = null)
    {
        return pieceTransform.transform.DOMove(position, _moveTime).SetEase(_ease).OnComplete(() =>
        {
            callback?.Invoke();
        });

    }
}
