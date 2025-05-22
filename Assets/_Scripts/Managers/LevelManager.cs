using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : PersistentSingleton<LevelManager>, IMessageHandle
{
    [SerializeField] private int _maxLevelCount;
    public int MaxLevelCount => _maxLevelCount;
    private int _currentLevel;

    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnGameWin, this);
        MessageManager.AddSubscriber(GameMessageType.OnLevelOpen, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnGameWin, this);
        MessageManager.RemoveSubscriber(GameMessageType.OnLevelOpen, this);
    }

    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt("Level " + _currentLevel + "Score", score);
    }
    public void UnlockNextLevel()
    {
        if (_currentLevel > _maxLevelCount)
        {
            return;
        }
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);
        ++_currentLevel;
        if (_currentLevel > levelReached)
        {
            PlayerPrefs.SetInt("LevelReached", _currentLevel);
            PlayerPrefs.Save();
        }
    }

    public void LoadNextLevel()
    {
        if (_currentLevel > _maxLevelCount)
        {
            SceneManager.LoadScene("Menu Scene");
        }
        else
        {
            SceneManager.LoadScene("Level " + _currentLevel);
        }
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnGameWin:
                int score = GameManager.Instance.CalculateScore();
                SaveScore(score);
                UnlockNextLevel();
                break;
            case GameMessageType.OnLevelOpen:
                int level = (int)message.data[0];
                _currentLevel = level;
                break;
        }
    }

}
