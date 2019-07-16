using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _points;

    [SerializeField]
    private int _playerNum;

    private Rigidbody _rb;
    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _movement;
    private IInput _input;

    private Vector3 _thisPlayerPosition;

    private GameController _gameController;

    private void Awake()
    {     
        _rb = GetComponent<Rigidbody>();

        _thisPlayerPosition = transform.position;
    }

    private void Start()
    {
        SetInputVariable();
        //SetPlayerControllerReferenceFromPlayer();
        _gameController = GetComponentInParent<GameController>();
    }

    private void SetInputVariable()
    {
        _playerNum = Convert.ToInt32(name);
        _input = new KeyboardInput(_playerNum);
        //_input = new SumKeyboardInput();
    }

    private int CountOfPlayers()
    {
        var gameController = GetComponentInParent<GameController>();
        return gameController.transform.childCount;
    }

    //передача ссылки на игрока в скрипт gameController.cs
    //void SetPlayerControllerReferenceFromPlayer() => _gameController.SetPlayerControllerReference(gameObject);

    private void Update() => ReadInput();

    private void ReadInput()
    {
        _moveHorizontal = _input.HorizontalMove;
        _moveVertical = _input.VerticalMove;
    }

    private void FixedUpdate() => MoveBall();

    private void MoveBall()
    {
        _movement.x = _moveHorizontal;
        _movement.z = _moveVertical;
        _rb.AddForce(_movement * 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("hole"))
        {
            _gameController.Spawn(_playerNum, GetComponent<MeshRenderer>().material.color);
            Destroy(this.gameObject);
        }
    }
}
