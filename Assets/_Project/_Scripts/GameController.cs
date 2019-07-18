using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(BallSpawner))]
public class GameController : MonoBehaviour
{
    private const int START_POINTS = 10;

    [SerializeField]
    private PlayerController _playerPrefab;

    [Range(0, 3), SerializeField]
    private int _playersCount;

    [Range(1, 3), SerializeField]
    private int roundsCount;

    [SerializeField]
    private List<Color> _playersColors;

    [Space]
    [SerializeField]
    private Scores _scores;

    [Space, Header("UI")] 
    [SerializeField]
    private GameObject _startPanel;

    [SerializeField]
    private Button _startBtn;

    [SerializeField]
    private Text _msgText;

    [SerializeField]
    private GameObject _resultsPanel;

    [SerializeField]
    private Text _timerText;

    private List<PlayerController> _players;
    private BallSpawner _ballSpawner;

    private int _roundNumber;

    private float _startTime;
    private float _time;

    private void Awake()
    {
        _timerText.gameObject.SetActive(false);

        _ballSpawner = GetComponent<BallSpawner>();

        _startBtn.onClick.AddListener(StartGame);

        _roundNumber = 0;
    }

    private void StartGame()
    {
        _startPanel.SetActive(false);
        CreateBalls( new List<IInput> {new KeyboardInput(0), new KeyboardInput(1) });

        StartCoroutine(GameLoop());
    }

    private void CreateBalls(List<IInput> inputs)
    {
        _players = new List<PlayerController>();

        for (int i = 0; i < inputs.Count; i++)
        {
            var player = Instantiate(_playerPrefab, transform);
            var color = _playersColors[UnityEngine.Random.Range(0, _playersColors.Count)];
            var scoreOnStart = START_POINTS + UnityEngine.Random.Range(0, _playersColors.Count) * 5;

            player.Init(i, color, scoreOnStart, inputs[i]);

            player.OnTrap += HandlePlayerOnTrap;
            player.OnFinish += PlayerOnFinish;
            player.OnCoin += BonusScore;

            _players.Add(player);
            _playersColors.Remove(color);
        }
    }

    private void HandlePlayerOnTrap(PlayerController player)
    {
        _ballSpawner.SpawnPlayer(player);
        player.Score -= _scores.Trap;
    }

    private void PlayerOnFinish(PlayerController player)
    {
        player.gameObject.SetActive(false);
        player.Score += _scores.Finish;
        player.TimeInSec += Math.Round(_time, 1);
    }

    private void BonusScore(PlayerController player) => player.Score += _scores.Coin;

  

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());

        yield return StartCoroutine(RoundPlaying());

        yield return StartCoroutine(RoundEnding());
    }

    private IEnumerator RoundStarting()
    {
        _roundNumber++;
        if (_roundNumber > 1)
            _players.ForEach(player => player.gameObject.SetActive(true));

        _ballSpawner.SpawnPlayers(_players);

        _players.ForEach(player => player.enabled = false);

        _msgText.text = "ROUND " + _roundNumber.ToString();
        yield return new WaitForSeconds(1);

        yield return StartCoroutine(Countdown(3));
    }

    private IEnumerator Countdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            _msgText.text = counter.ToString();
            yield return new WaitForSeconds(1);
            counter--;
        }
        _msgText.text = "GO!";
        yield return new WaitForSeconds(1);
    }

    private IEnumerator RoundPlaying()
    {
        _msgText.text = string.Empty;
        _players.ForEach(player => player.enabled = true);
        _players.ForEach(player => player.GetComponent<Rigidbody>().isKinematic = false);

        _startTime = Time.time;
        _timerText.gameObject.SetActive(true);

        while (!isAllPlayersNotActive())
        {
            yield return null;
        }

        bool isAllPlayersNotActive() => _players.TrueForAll(player => !player.gameObject.activeSelf);
    }

    private IEnumerator RoundEnding()
    {
        _timerText.gameObject.SetActive(false);

        _players.ForEach(player => player.GetComponent<Rigidbody>().isKinematic = true);
        //_players.ForEach(player => player.TimeInSec += );

        if (_roundNumber < roundsCount)
            yield return StartCoroutine(GameLoop());
        else
            yield return StartCoroutine(ResultsTable());
    }

    private IEnumerator ResultsTable()
    {
        _resultsPanel.SetActive(true);
        yield return new WaitForSeconds(3);
        _resultsPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        ChangeTime();

        void ChangeTime()
        {
            _time = Time.time - _startTime;
            int seconds = (int)(_time % 60);
            int minutes = (int)(_time / 60);

            _timerText.text = String.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}