using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    //public IDictionary<string, int> _pointsAllPlayers;

    public List<int> _pointsAllPlayers;

    [SerializeField]
    private int _countOfPlayers;

    private GameObject _gamePlayer;
    private GameObject _playerResource;
    private GameObject _player;
    private GameObject[] _players;

    private Vector3 _position;
    private Vector3[] corners = { new Vector3(-34, 1, -15), new Vector3(-34, 1, 15),
                                  new Vector3(34, 1, -15),  new Vector3(34, 1, 15) };
    private GameController _gameController;

    public void SpawnAllPlayers()
    {
        _players = new GameObject[_countOfPlayers];
        _gameController = GetComponentInParent<GameController>();
        _pointsAllPlayers = new List<int>(_countOfPlayers);

        for (int i = 0; i < _countOfPlayers; i++)
        {
            var _color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
            _players[i] = Spawn(i, _color);
            _pointsAllPlayers.Add(10); //пока на старте по 10 очков
        }

        //GetCountOfPlayers(_countOfPlayers);
        //GetPlayersReference(_players);

        //void GetPlayersReference(GameObject[] players) => _gameController.SetPlayersReference(players);
        //void GetCountOfPlayers(int count) => _gameController.SetCountOfPlayers(count);
    }

    private void SetPosition(int i) => _position = corners[i];

    public GameObject Spawn(int playerNum, Color color)
    {
        _playerResource = Resources.Load<GameObject>("Player") as GameObject;
        SetPosition(playerNum);
        _player = Instantiate(_playerResource, _position, Quaternion.identity) as GameObject;
        _player.name = playerNum.ToString();
        _player.transform.SetParent(this.transform);
        _player.transform.GetComponent<MeshRenderer>().material.color = color;
        return _player;
    } 
    
}