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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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

    
}
