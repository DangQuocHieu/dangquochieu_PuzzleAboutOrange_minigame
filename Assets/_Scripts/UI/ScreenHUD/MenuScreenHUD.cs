using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreenHUD : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _tutorialButton;
    void Start()
    {
        AddButtonListener();
    }

    private void AddButtonListener()
    {
        _playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("Level Selection Scene");
        });
        _tutorialButton.onClick.AddListener(() =>
        {
            ScreenManager.Instance.ShowScreen(ScreenKey.Tutorial);
        });
    }
}
