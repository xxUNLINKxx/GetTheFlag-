using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class playerData : NetworkBehaviour
{
    public List<Color> colors = new List<Color>();
    public List<int> playerID = new List<int>();
    public GameObject[] allPlayers;
    void Start()
    {
        StartCoroutine(SetColor(0.2f));    }

    public IEnumerator SetColor(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        allPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in allPlayers)
        {
            player pP = p.GetComponent<player>();
            playerScore pS = p.GetComponent<playerScore>();
            pP.SetColor(colors[0]);
            pS.SetColor(colors[0]);
            colors.RemoveAt(0);
            pP.SetID(playerID[0]);
            playerID.RemoveAt(0);
        }
    }
}
