using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public GameObject Instance;

    private int _playerNum;
    private Rigidbody _rb;

    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _movement;

    private IInput _input;

    private Vector3 _thisPlayerPosition;

    private BallSpawner _ballSpawner;

    private void Start()
    {
        _ballSpawner = GetComponentInParent<BallSpawner>();
        _rb = GetComponent<Rigidbody>();
        _thisPlayerPosition = transform.position;
        SetInputVariable();
    }

    private void SetInputVariable()
    {
        _playerNum = Convert.ToInt32(name);
        _input = new KeyboardInput(_playerNum);
        //_input = new SumKeyboardInput();
    }

    private int CountOfPlayers() => _ballSpawner.transform.childCount;  

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
            _ballSpawner.Spawn(_playerNum, GetComponent<MeshRenderer>().material.color);
            _ballSpawner._pointsAllPlayers[_playerNum] -= 1;
            Destroy(this.gameObject);
        }
        else if (other.tag.Equals("finish"))
        {
            this.gameObject.SetActive(false);
            _ballSpawner._pointsAllPlayers[_playerNum] += 5;
        }
    }
}
