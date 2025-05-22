using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinScreenHUD : UIScreen, IMessageHandle
{
    [Header("Buttons")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private RectTransform _starContainer;

    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnGameWin, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnGameWin, this);
    }

    void Start()
    {
        AddButtonListener();
    }

    private void AddButtonListener()
    {
        _restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        });
        _homeButton.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync("Menu Scene");
        });
        _nextLevelButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.LoadNextLevel();
        });
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnGameWin:
                int score = (int)message.data[0];
                ShowScoreUI(score);
                break;
        }
    }

    private void ShowScoreUI(int score)
    {
        foreach (RectTransform item in _starContainer)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < score; i++)
        {
            _starContainer.GetChild(i).gameObject.SetActive(true);
        }
    }
}
