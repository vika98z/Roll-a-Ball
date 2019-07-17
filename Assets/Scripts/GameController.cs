using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BallSpawner))]
public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPanel;

    [SerializeField]
    private Button _startBtn;

    [SerializeField]
    private int _currentCountOfPlayers;

    private GameObject[] _players;
    private BallSpawner _ballSpawner;
    private PlayerController _playerController;

    private void Awake()
    {
        _ballSpawner = GetComponent<BallSpawner>();

        _startBtn.onClick.AddListener(StartGame);
    }

    private void Update()
    {
        _currentCountOfPlayers = PlayerController.Count;
    }

    private void StartGame()
    {
        _startPanel.SetActive(false);
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
        _ballSpawner.SpawnAllPlayers();
        yield return null;
    }

    private IEnumerator RoundPlaying()
    {
        while (PlayerController.Count != 0)
        {

            yield return null;
        }       
    }

    private IEnumerator RoundEnding()
    {
        yield return null;
        _startPanel.SetActive(true);
    }

    //public void SetPlayersReference(GameObject[] players)
    //{
    //    _players = new GameObject[_currentCountOfPlayers];
    //    _players = players;
    //}

    //public void SetCountOfPlayers(int count) => _countOfPlayers = count;
}