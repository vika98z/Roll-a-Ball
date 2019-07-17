using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BallSpawner))]
public class GameController : MonoBehaviour
{
    private const int START_POINTS = 10;

    [SerializeField]
    private PlayerController _playerPrefab;

    [Range(0, 3), SerializeField]
    private int _playersCount;

    [SerializeField]
    private List<Color> _playersColors;

    [Space, Header("UI"), SerializeField]
    private GameObject _startPanel;

    [SerializeField]
    private Button _startBtn;

    private List<PlayerController> _players;
    private BallSpawner _ballSpawner;

    private void Awake()
    {
        _ballSpawner = GetComponent<BallSpawner>();
        //StartGame();
        _startBtn.onClick.AddListener(StartGame);
    }

    private void Init()
    {
        _players = new List<PlayerController>();

        for (int i = 0; i < _playersCount; i++)
        {
            var player = Instantiate(_playerPrefab, transform);
            var color = _playersColors[Random.Range(0, _playersColors.Count)];
            var scaleFactor = START_POINTS + Random.Range(0, _playersColors.Count) * 5;
            
            player.Init(i,  color, scaleFactor);
            player.OnTrap += HandlePlayerOnTrap;
            player.enabled = false;

            _players.Add(player);
            _playersColors.Remove(color);
        }
    }

    private void HandlePlayerOnTrap(PlayerController player) => _ballSpawner.SpawnPlayer(player);

    private void StartGame()
    {
        _startPanel.SetActive(false);
        Init();

        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());

        yield return StartCoroutine(RoundPlaying());

        yield return StartCoroutine(RoundEnding());
    }

    private IEnumerator RoundStarting()
    {
        _ballSpawner.SpawnPlayers(_players);
        yield return null;

        /// _players[i].enabled = true;
    }

    private IEnumerator RoundPlaying()
    {
        while (_players.Count != 0)
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        /// _players[i].enabled = false;
        yield return null;
        _startPanel.SetActive(true);
    }
}