using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : PersistentSingleton<ScreenManager>, IMessageHandle
{
    private Dictionary<ScreenKey, UIScreen> _uiScreenDictionary = new Dictionary<ScreenKey, UIScreen>();

    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnGameWin, this);
        MessageManager.AddSubscriber(GameMessageType.OnGameOver, this);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        InitializeScreensDictionary();
    }
    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnGameWin, this);
        MessageManager.RemoveSubscriber(GameMessageType.OnGameOver, this);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        HideAllScreen();
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnGameWin:
                ShowScreen(ScreenKey.GameWin);
                break;
            case GameMessageType.OnGameOver:
                ShowScreen(ScreenKey.GameOver);
                break;
        }
    }

    private void InitializeScreensDictionary()
    {
        foreach (Transform screen in transform)
        {
            screen.gameObject.SetActive(true);
        }
        foreach (UIScreen screen in transform.GetComponentsInChildren<UIScreen>())
            {
                if (!_uiScreenDictionary.ContainsKey(screen.Key))
                {
                    _uiScreenDictionary.Add(screen.Key, screen);
                    screen.gameObject.SetActive(false);
                }
            }
    }

    public void ShowScreen(ScreenKey key)
    {
        _uiScreenDictionary[key].Show();
    }

    public void HideScreen(ScreenKey key)
    {
        _uiScreenDictionary[key].Hide();
    }

    public void HideAllScreen()
    {
        foreach (var key in _uiScreenDictionary.Keys)
        {
            HideScreen(key);
        }
    }

}
