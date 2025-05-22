using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionScreenHUD : MonoBehaviour
{
    [SerializeField] private LevelSelectionButton _levelButtonPrefab;
    [SerializeField] private RectTransform _levelButtonContainer;
    private List<LevelSelectionButton> _selectionButtonList = new List<LevelSelectionButton>();


    void Start()
    {
        InitializeLevelButtons();
    }

    private void InitializeLevelButtons()
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);
        int maxLevelCount = LevelManager.Instance.MaxLevelCount;
        for (int i = 0; i < maxLevelCount; i++)
        {
            LevelSelectionButton button = Instantiate(_levelButtonPrefab, _levelButtonContainer);
            button.Initialize(i + 1);
            _selectionButtonList.Add(button);
            button.GetComponent<Button>().interactable = false;
        }

        for(int i = 0; i < levelReached; i++)
        {
            _selectionButtonList[i].GetComponent<Button>().interactable = true;
        }
    }
}
