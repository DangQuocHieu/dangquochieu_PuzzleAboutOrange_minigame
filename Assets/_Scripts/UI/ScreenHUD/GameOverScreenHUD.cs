using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenHUD : UIScreen
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    void Start()
    {
        AddButtonListener();
    }
    private void AddButtonListener()
    {
        _restartButton.onClick.AddListener(() =>
        {
            ScreenManager.Instance.HideAllScreen();
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        });
        _homeButton.onClick.AddListener(() =>
        {
            ScreenManager.Instance.HideAllScreen();
            SceneManager.LoadSceneAsync("Menu Scene");
        });
    }

}
