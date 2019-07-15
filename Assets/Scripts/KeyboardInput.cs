using UnityEngine;

public class KeyboardInput : IInput
{
    private string _hMove, _vMove;

    public KeyboardInput(int playerNum)
    {
        _hMove = $"Horizontal + {playerNum}";
        _vMove = $"Vertical + {playerNum}";
    }

    public float HorizontalMove => Input.GetAxis(_hMove);
    public float VerticalMove => Input.GetAxis(_vMove);
}
