using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject StartPanel;

    private GameObject[] _players;
    private int _countOfPlayers;

    void Start()
    {
        //StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundPlaying());
    }

    private IEnumerator RoundPlaying()
    {
        while (CountOfActivePlayers() != 0)
        {

            yield return null;
        }
        StartPanel.SetActive(true);
    }

    int CountOfActivePlayers()
    {
        int res = 0;
        foreach (var player in _players)
        {
            if (player.activeInHierarchy)
                res++;
        }
        return res;
    }

    public void SetPlayersReference(GameObject[] players)
    {
        _players = new GameObject[_countOfPlayers];
        _players = players;
    }

    public void SetCountOfPlayers(int count) => _countOfPlayers = count;
}