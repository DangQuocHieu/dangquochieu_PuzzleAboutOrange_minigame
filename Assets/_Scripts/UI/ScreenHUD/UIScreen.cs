using System.Collections;
using System.ComponentModel.Design;
using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;

public enum ScreenKey
{
    GameWin,
    GameOver,
    Tutorial
}
public abstract class UIScreen : MonoBehaviour
{
    [SerializeField] protected ScreenKey _key;
    public ScreenKey Key => _key;
    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(OnShowCoroutine());
    }

    public void Hide()
    {
        StartCoroutine(OnHideCoroutine());
    }

    protected virtual IEnumerator OnShowCoroutine()
    {
        yield return new WaitForEndOfFrame();
    }
    protected virtual IEnumerator OnHideCoroutine()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
}
