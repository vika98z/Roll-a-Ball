public interface IInput
{
    float HorizontalMove { get; } 
    float VerticalMove { get; }
}

public interface IPlayer : IInput
{
    int StartScore { get; }
}
