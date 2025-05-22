using System.Collections;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameWinScreenHUD : UIScreen
{
    [Header("Buttons")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _nextLevelButton;

    [Header("Animation Config")]
    [SerializeField] private ScaleAnimationSO _scaleSO;
    [SerializeField] private ScaleAnimationSO _scaleStarSO;
    [SerializeField] private SlideAnimationSO _slideSO;

    [Header("Panel")]
    [SerializeField] private RectTransform _starContainer;
    [SerializeField] private RectTransform _popupPanel;

    void Start()
    {
        AddButtonListener();
    }

    void OnEnable()
    {
        _popupPanel.transform.localScale = Vector3.zero;
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

    // private IEnumerator RestartGameCoroutine()
    // {
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    //     asyncLoad.allowSceneActivation = false;

    //     yield return _scaleSO.ScaleOut(_popupPanel).WaitForCompletion();

    //     asyncLoad.allowSceneActivation = true;
    //     yield return _slideSO.SlideOut(GetComponent<RectTransform>()).WaitForCompletion();
    //     yield return new WaitForEndOfFrame();
    //     gameObject.SetActive(false);
    // }


    private void HideScoreUI()
    {
        foreach (RectTransform item in _starContainer)
        {
            item.gameObject.SetActive(false);
        }

    }

    protected override IEnumerator OnShowCoroutine()
    {
        HideScoreUI();
        yield return _slideSO.SlideIn(GetComponent<RectTransform>()).WaitForCompletion();
        yield return _scaleSO.ScaleIn(_popupPanel).WaitForCompletion();
        int score = GameManager.Instance.CalculateScore();
        for (int i = 0; i < score; i++)
        {
            _starContainer.GetChild(i).gameObject.SetActive(true);
            yield return _scaleStarSO.ScaleIn(_starContainer.GetChild(i).transform).WaitForCompletion();
        }
        yield return base.OnShowCoroutine();
    }

    protected override IEnumerator OnHideCoroutine()
    {
        yield return _scaleSO.ScaleOut(_popupPanel).WaitForCompletion();
        yield return _slideSO.SlideOut(GetComponent<RectTransform>()).WaitForCompletion();
        yield return base.OnHideCoroutine();
    }

}
