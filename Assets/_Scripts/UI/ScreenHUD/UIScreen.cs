using System.ComponentModel.Design;
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
    [SerializeField] private ScreenKey _key;
    public ScreenKey Key => _key;
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
