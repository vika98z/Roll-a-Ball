using UnityEngine;

[CreateAssetMenu(fileName = "ScoresData", menuName = "Data/Scores", order = 52)]
public class Scores : ScriptableObject
{
    [SerializeField]
    private int _finish;

    [SerializeField]
    private int _trap;

    [SerializeField]
    private int _coin;

    public int Finish => _finish;
    public int Trap => _trap;
    public int Coin => _coin;
}
