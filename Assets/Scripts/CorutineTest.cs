using System.Collections;
using UnityEngine;

public class CorutineTest : MonoBehaviour
{
    Coroutine coroute;

    IEnumerator coroute2;

    private void Awake()
    {
        //StartCoroutine(MyCorute());

        // Вызов версия 1
        //StartCoroutine(GameLoop());

        // Вызов версия 2
        StartCoroutine(nameof(GameLoop), "start0"); //StartCoroutine("GameLoop");

        // Вызов версия 3
        coroute = StartCoroutine(GameLoop("start1"));

        coroute2 = GameLoop("start2");
        StartCoroutine(coroute2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StopAllCoroutines();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            StopCoroutine(nameof(GameLoop)); //StopCoroutine("GameLoop");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            StopCoroutine(coroute);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            StopCoroutine(coroute2);
    }

    #region Coroutines
    private IEnumerator GameLoop(string startMessage)
    {
        Debug.Log(startMessage);
        yield return StartCoroutine(MyCorute());

        Debug.Log("starting GameLoop");
        yield return StartCoroutine(RoundPlaying());

        Debug.Log("finish GameLoop");
    }

    private IEnumerator RoundPlaying()
    {
        while (!HaveWinner())
        {
            yield return null;
        }
        //yield return new WaitUntil(HaveWinner);

        bool HaveWinner()
        {
            // логика проверки победителя
            return false;
        }
    }

    private IEnumerator MyCorute()
    {
        int i = 0;

        while (i < 10)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0)) i++;

            if (Input.GetKeyDown(KeyCode.Escape))
                yield break;

            Debug.Log(i);
            yield return null;
        }


        Debug.Log("finish MyCorute");
    }
    #endregion
}
