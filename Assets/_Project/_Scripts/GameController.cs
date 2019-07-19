using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayer : IPlayer
{
    KeyboardInput _input;

    public TestPlayer(KeyboardInput input, int StartScore)
    {
        _input = input;
        this.StartScore = StartScore;
    }

    public int StartScore { get; private set; }
    public float HorizontalMove => _input.HorizontalMove;
    public float VerticalMove => _input.VerticalMove;
}

public enum GameStates
{
   RoundStart, RoundPlay, RoundEnd
}

[RequireComponent(typeof(BallSpawner))]
public class GameController : MonoBehaviour
{
    public event Action<GameStates> OnGameStateChanged;

    [SerializeField]
    private PlayerController _playerPrefab;

    [Range(1, 3), SerializeField]
    private int _roundsCount;

    [SerializeField]
    private List<Color> _playersColors;

    [Space]
    [SerializeField]
    private Scores _scores;

    private List<PlayerController> _players;
    private BallSpawner _ballSpawner;

    private float _startTime;

    private GameStates _gameState;

    public GameStates GameState
    {
        get => _gameState;
        private set
        {
            _gameState = value;
            OnGameStateChanged?.Invoke(GameState);
        }
    }

    public float Time { get; private set; }

    public float StartDelay => 4f;

    [ContextMenu("Test Start")]
    public void TestStart()
    {
        StartGame(
          new List<IPlayer>
          {
            new TestPlayer(new KeyboardInput(0), 10),
            new TestPlayer(new KeyboardInput(1), 5)
          }
          );
    }

    public void StartGame(List<IPlayer> players)
    {
        CreateBalls();
        StartCoroutine(GameLoop());

        void CreateBalls()
        {
            _players = new List<PlayerController>();

            foreach (IPlayer playerData in players)
            {
                var player = Instantiate(_playerPrefab, transform);
                var color = _playersColors[UnityEngine.Random.Range(0, _playersColors.Count)];
                var scoreOnStart = playerData.StartScore + UnityEngine.Random.Range(0, _playersColors.Count) * 5;

                player.Init(color, scoreOnStart, playerData);

                player.OnTrap += HandlePlayerOnTrap;
                player.OnFinish += PlayerOnFinish;
                player.OnCoin += BonusScore;

                _players.Add(player);
                _playersColors.Remove(color);
            }

            void HandlePlayerOnTrap(PlayerController player)
            {
                _ballSpawner.SpawnPlayer(player);
                player.Score -= _scores.Trap;
            }

            void PlayerOnFinish(PlayerController player)
            {
                player.gameObject.SetActive(false);
                player.Score += _scores.Finish;
                player.TimeInSec += Math.Round(Time, 1);
            }

            void BonusScore(PlayerController player) => player.Score += _scores.Coin;
        }
    }

    private void Awake() => _ballSpawner = GetComponent<BallSpawner>();

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());

        yield return StartCoroutine(RoundPlaying());

        yield return StartCoroutine(RoundEnding());
    }

    private IEnumerator RoundStarting()
    {
        GameState = GameStates.RoundStart;

        _ballSpawner.SpawnPlayers(_players);

        _players.ForEach(player => player.enabled = false);

        yield return new WaitForSeconds(StartDelay);
    }

    private IEnumerator RoundPlaying()
    {
        GameState = GameStates.RoundPlay;

        _startTime = UnityEngine.Time.time;

        _players.ForEach(player => player.enabled = true);
        _players.ForEach(player => player.GetComponent<Rigidbody>().isKinematic = false);

        while (!isAllPlayersNotActive())
            yield return null;

        bool isAllPlayersNotActive() => _players.TrueForAll(player => !player.gameObject.activeSelf);
    }

    private IEnumerator RoundEnding()
    {
        GameState = GameStates.RoundEnd;

        _players.ForEach(player => player.GetComponent<Rigidbody>().isKinematic = true);

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update() => Time = UnityEngine.Time.time - _startTime;
}