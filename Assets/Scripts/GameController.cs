using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject _player;

    private GameObject _player1;
    private GameObject _player2;
    private GameObject _player3;

    public Vector3 _position1;
    private Vector3 _position2;
    private Vector3 _position3;

    private void Awake()
    {
        _position1 = new Vector3(-34, 1, -15);
        _position2 = new Vector3(-34, 1, 15);
        _position3 = new Vector3(34, 1, -15);
        Spawn();
    }

    void Spawn()
    {
        _player = Resources.Load<GameObject>("Player") as GameObject;
        _player1 = Instantiate(_player, _position1, Quaternion.identity) as GameObject;
        _player1.name = "Player 1";
        _player2 = Instantiate(_player, _position2, Quaternion.identity) as GameObject;
        _player2.name = "Player 2";
        _player3 = Instantiate(_player, _position3, Quaternion.identity) as GameObject;
        _player3.name = "Player 3";
    }
}
