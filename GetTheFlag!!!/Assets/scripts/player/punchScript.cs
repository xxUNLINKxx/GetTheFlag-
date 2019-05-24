using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class punchScript : MonoBehaviour
{
    public float punchRange;
    public bool canPunch = true;
    public GameObject thisPlayer;
    public GameObject player;
    public float punchTimer;
    private float startpT;

    private void Update()
    {
        DetectOpponent();
        if (Input.GetMouseButtonDown(0))
        {
            Punch(player);
        }
        if (!canPunch)
        {
            startpT -= Time.deltaTime;
            if (startpT <= 0)
            {
                canPunch = true;
            }
        }
    }

    void DetectOpponent()
    {
        RaycastHit2D opPlayer = Physics2D.Raycast(transform.position, transform.right, punchRange);
        if (opPlayer)
        {
            if (opPlayer.transform.CompareTag("Player"))
            {
                player = opPlayer.transform.gameObject;
            }
            else
            {
                player = null;
            }
        }
        else
        {
            player = null;
        }
    }


    void Punch(GameObject opponent)
    {
        if (opponent != null&&canPunch)
        {
            playerData o_p = GameObject.Find("PlayerData").GetComponent<playerData>();
            opponent.GetComponent<player>().TakeDamage();
            canPunch = false;
            startpT = punchTimer;
        }
    }

}
