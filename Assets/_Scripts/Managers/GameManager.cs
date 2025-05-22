using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float _countdownTimer;
    private float _remainingTime;
    public float RemainingTime => _remainingTime;

    void Start()
    {
        _remainingTime = _countdownTimer;
    }

    void Update()
    {
        UpdateRemainingTime();
    }

    private void UpdateRemainingTime()
    {
        _remainingTime -= Time.deltaTime;
        if (_remainingTime <= 0f)
        {
            int score = CalculateScore();
            MessageManager.SendMessage(new Message(GameMessageType.OnGameOver));
        }
    }

    public int CalculateScore()
    {
        return 3;
    }
    

}
