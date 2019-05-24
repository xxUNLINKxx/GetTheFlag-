using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class flag : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isHeld;
    public GameObject player;
    public int points;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isHeld)
        {
            if (other.CompareTag("Player"))
            {
                isHeld = true;
                player = other.gameObject;
            }
        }
    }

    private void FixedUpdate()
    {
        if (player != null && isHeld)
        {
            player.GetComponent<playerScore>().Cmd_addPoints(points);
            transform.position = player.transform.GetChild(3).transform.position;
            transform.localPosition = new Vector2(-0.5f, 0);
            rb.velocity = Vector2.zero;
        }
        if (!player.GetComponent<player>().Alive)
        {
            player = null;
            isHeld = false;
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            int rand = Random.Range(0,2);
            if(rand == 0)
            {
                rb.AddForce(Vector2.left * 1, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.right * 1, ForceMode2D.Impulse);
            }
        }
    }
}
