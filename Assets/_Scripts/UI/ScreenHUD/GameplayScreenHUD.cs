using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayScreenHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _remainingTimeText;
    [Header("Button")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    void Start()
    {
        AddButtonListener();
    }
    void Update()
    {
        UpdateRemainingTimeText();
    }

    private void UpdateRemainingTimeText()
    {
        float remainingTime = GameManager.Instance.RemainingTime;
        _remainingTimeText.text = "00 : " + Mathf.RoundToInt(remainingTime);

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
    }
}
