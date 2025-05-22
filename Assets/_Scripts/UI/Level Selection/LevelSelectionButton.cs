using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour
{
    private int _levelToOpen;
    public int LevelToOpen => _levelToOpen;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private RectTransform _starContainer;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            MessageManager.SendMessage(new Message(GameMessageType.OnLevelOpen, new object[] { _levelToOpen }));
            SceneManager.LoadSceneAsync("Level " + _levelToOpen);
        });
    }
    public void Initialize(int levelToOpen)
    {
        _levelToOpen = levelToOpen;
        _titleText.text = "Level " + (levelToOpen).ToString();
        int score = PlayerPrefs.GetInt("Level " + _levelToOpen + "Score", 0);
        ShowScoreUI(score);
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
