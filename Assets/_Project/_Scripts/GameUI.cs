using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameController _gameController;

    [SerializeField]
    private GameObject _startPanel;

    [SerializeField]
    private Button _startBtn;

    [SerializeField]
    private Text _timerText;

    [SerializeField]
    private GameObject _resultsPanel;

    [SerializeField]
    private Text _msgText;

    private Coroutine _roundTimerCoroute;

    void Awake()
    {
        _startBtn.onClick.AddListener(StartGameManually);
        _gameController.OnGameStateChanged += HandleGameState;

        void StartGameManually()
        {
            _gameController.TestStart();
            _startPanel.SetActive(false);
        }
    }

    private void HandleGameState(GameStates state)
    {
        switch (state)
        {
            case GameStates.RoundStart:
                StartCoroutine(Countdown(_gameController.StartDelay));
                break;
            case GameStates.RoundPlay:
                _msgText.text = string.Empty;
                _roundTimerCoroute = StartCoroutine(Timer());
                break;
            case GameStates.RoundEnd:
                _timerText.text = string.Empty;
                StopCoroutine(_roundTimerCoroute);
                _resultsPanel.SetActive(true);
                break;
            default:
                break;
        }

        IEnumerator Countdown(float seconds)
        {
            while (seconds > 1)
            {
                _msgText.text = seconds.ToString();
                seconds--;
                yield return new WaitForSeconds(1);
            }

            _msgText.text = "GO!";
            yield return new WaitForSeconds(1);
        }

        IEnumerator Timer()
        {
            _timerText.text = string.Empty;

            while (true)
            {
                int seconds = (int)(_gameController.Time % 60);
                int minutes = (int)(_gameController.Time / 60);
                _timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);

                yield return null;
            }
        }
    }
}
