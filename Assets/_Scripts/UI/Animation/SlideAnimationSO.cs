using DG.Tweening;
using UnityEngine;
[CreateAssetMenu(fileName = "SlideAnimationSO", menuName = "Scriptable Objects/UIAnimationSO/SlideAnimationSO")]
public class SlideAnimationSO : ScriptableObject
{
    [SerializeField] private Vector2 _fromPosition;
    [SerializeField] private Vector2 _toPosition;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _inEase;
    [SerializeField] private Ease _outEase;

    public Tweener SlideIn(RectTransform target)
    {
        return target.DOAnchorPos(_toPosition, _duration).From(_fromPosition).SetEase(_inEase);
    }

    public Tweener SlideOut(RectTransform target)
    {
        return target.DOAnchorPos(_fromPosition, _duration).From(_toPosition).SetEase(_outEase);
    }
}
