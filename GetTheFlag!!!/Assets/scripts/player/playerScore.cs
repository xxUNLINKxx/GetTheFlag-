using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class playerScore : NetworkBehaviour
{
    [SyncVar]
    public int score;
    private float i;
    public Text pScore;

    [Command]
    public void Cmd_addPoints(int point)
    {
        i += point;
        score = (int)(i * 0.01f);
    }

    private void FixedUpdate()
    {
        pScore.transform.localScale = transform.localScale;
        pScore.GetComponent<Text>().text = score.ToString();
        pScore.GetComponent<Text>().color = color;
    }

    [SyncVar]
    Color color;

    [Command]
    void Cmd_Color(Color c)
    {
        color = c;
    }

    public void SetColor(Color c)
    {
        Cmd_Color(c);
    }
}


