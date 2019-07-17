using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<PlayerController> OnTrap; 

    private int _playerNum;
    private Rigidbody _rb;

    private float _moveHorizontal;
    private float _moveVertical;
    private Vector3 _movement;

    private IInput _input;

    private Vector3 _thisPlayerPosition;

    private BallSpawner _ballSpawner;

    public void Init(int nameNum, Color color, int points)
    {
        transform.localScale *= points / 10f;
        name = nameNum.ToString();
        GetComponent<MeshRenderer>().material.color = color;
    }

    private void Awake()
    {
        _ballSpawner = GetComponentInParent<BallSpawner>();
        _rb = GetComponent<Rigidbody>();
    }
    
    private void Start()
    {
        _thisPlayerPosition = transform.position;
        SetInputVariable();

        void SetInputVariable()
        {
            _playerNum = Convert.ToInt32(name);
            _input = new KeyboardInput(_playerNum);
            //_input = new SumKeyboardInput();
        }
    }

    private void Update()
    {
        ReadInput();

        void ReadInput()
        {
            _moveHorizontal = _input.HorizontalMove;
            _moveVertical = _input.VerticalMove;
        }
    }

    private void FixedUpdate()
    {
        MoveBall();

        void MoveBall()
        {
            _movement.x = _moveHorizontal;
            _movement.z = _moveVertical;
            _rb.AddForce(_movement * 10f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("hole"))
        {
           // _ballSpawner.SpawnOnePlayer(_playerNum, GetComponent<MeshRenderer>().material.color);
           // _ballSpawner._pointsAllPlayers[_playerNum] -= 1;
            OnTrap?.Invoke(this);
        }
        else if (other.tag.Equals("finish"))
        {
            //_ballSpawner._pointsAllPlayers[_playerNum] += 5;
            Destroy(this.gameObject);
        }
    }
}
