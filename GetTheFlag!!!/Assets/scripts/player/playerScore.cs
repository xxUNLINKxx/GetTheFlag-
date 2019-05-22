using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror
{
    public class playerScore : NetworkBehaviour
    {
        [SyncVar]
        public int score;

        public Text pScore;

        [Command]
        public void Cmd_addPoints(int point)
        {
            score += point;
        }

        private void FixedUpdate()
        {
            pScore.GetComponent<Text>().text = score.ToString();
        }
    }
}

