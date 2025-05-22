using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "ScaleAnimationSO", menuName = "Scriptable Objects/UIAnimationSO/ScaleAnimationSO")]
public class ScaleAnimationSO : ScriptableObject
{
    [SerializeField] private float _fromValue;
    [SerializeField] private float _toValue;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _inEase;
    [SerializeField] private Ease _outEase;

    public Tweener ScaleIn(Transform target)
    {
        target.transform.localScale = Vector3.one * _fromValue;
        return target.DOScale(Vector3.one * _toValue, _duration).From(Vector3.one * _fromValue).SetEase(_inEase);
    }

    public Tweener ScaleOut(Transform target)
    {
        return target.DOScale(Vector3.one * _fromValue, _duration).From(Vector3.one * _toValue).SetEase(_outEase);
    }
}
