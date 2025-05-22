using UnityEngine;
using UnityEngine.UI;

public class TutorialScreenHUD : UIScreen
{
    [SerializeField] private Button _gobackButton;

    void Start()
    {
        AddButtonListener();
    }

    private void AddButtonListener()
    {
        _gobackButton.onClick.AddListener(() =>
        {
            ScreenManager.Instance.HideScreen(ScreenKey.Tutorial);
        });
    }
}
