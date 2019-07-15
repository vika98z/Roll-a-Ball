using UnityEngine;

public class KeyboardInput : IInput
{
    private string _hMove, _vMove;

    public KeyboardInput(int playerNum)
    {
        _hMove = "Horizontal" + playerNum.ToString();
        _vMove = "Vertical" + playerNum.ToString();
    }

    public float HorizontalMove => Input.GetAxis(_hMove);
    public float VerticalMove => Input.GetAxis(_vMove);
}
