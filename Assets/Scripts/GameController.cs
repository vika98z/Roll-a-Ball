using UnityEngine;

public class GameController : MonoBehaviour
{
    public int CountOfPlayers;

    private GameObject _gamePlayer;
    private GameObject _playerResource;
    private GameObject _player;

    private Vector3 _position;
    private Vector3[] corners = { new Vector3(-34, 1, -15), new Vector3(-34, 1, 15), new Vector3(34, 1, -15), new Vector3(34, 1, 15) };

    private void Awake()
    {
        for (int i = 0; i < CountOfPlayers; i++)
        {
            var _color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            Spawn(i, _color);
        }
    }

    private void SetPosition(int i)
    {
        _position = corners[i];
    }

    public void Spawn(int playerNum, Color color)
    {
        _playerResource = Resources.Load<GameObject>("Player") as GameObject;
        SetPosition(playerNum);
        _player = Instantiate(_playerResource, _position, Quaternion.identity) as GameObject;
        _player.name = playerNum.ToString();
        _player.transform.SetParent(this.transform);
        _player.transform.GetComponent<MeshRenderer>().material.color = color;
    }

    public void SetPlayerControllerReference(GameObject player)
    {
        _gamePlayer = player;
    }
}