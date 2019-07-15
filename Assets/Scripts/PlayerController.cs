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

    //
    public Vector3 _position1;
    private Vector3 _position2;
    private Vector3 _position3;
    //

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //
        _position1 = new Vector3(-34, 1, -15);
        _position2 = new Vector3(-34, 1, 15);
        _position3 = new Vector3(34, 1, -15);
        //
    }

    private void Start()
    {
        SetInputVariable();
        SetColor();
    }

    private void SetInputVariable()
    {
        string[] parts = name.Split(' ');
        _playerNum = Convert.ToInt32(parts[1]) - 1;
        _input = new KeyboardInput(_playerNum);
    }

    private void SetColor() => GetComponent<MeshRenderer>().material.color = 
        new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));

    //private void SetStartTransform()
    //{
    //    float pos = 2 - _points / 10f;

    //    transform.localScale = new Vector3(pos, pos, pos);
    //    transform.position = new Vector3(transform.position.x, pos / 2f, transform.position.z);
    //}

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
            Destroy(this.gameObject);
            GameObject _player = Resources.Load<GameObject>("Player") as GameObject;
            //
            GameObject _player1;
            GameObject _player2;
            GameObject _player3;
            //
            switch (_playerNum)
            {
                case 0:
                    _player1 = Instantiate(_player, _position1, Quaternion.identity) as GameObject;
                    _player1.name = "Player 1";
                    break;
                case 1:
                    _player2 = Instantiate(_player, _position2, Quaternion.identity) as GameObject;
                    _player2.name = "Player 2";
                    break;
                case 2:
                    _player3 = Instantiate(_player, _position3, Quaternion.identity) as GameObject;
                    _player3.name = "Player 3";
                    break;
                default:
                    break;
            }
        }
    }
}
