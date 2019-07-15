using UnityEngine;

public class SumKeyboardInput : IInput
{
    public float HorizontalMove => Input.GetAxis("Horizontal0") + Input.GetAxis("Horizontal1") + Input.GetAxis("Horizontal2");
    public float VerticalMove => Input.GetAxis("Vertical0") + Input.GetAxis("Vertical1") + Input.GetAxis("Vertical2");
}