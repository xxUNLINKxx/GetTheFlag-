using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mirror
{
    public class Play : MonoBehaviour
    {
        private bool pressed;
        private goal GetGoal;

        private void Start()
        {
            GetGoal = GetComponent<goal>();
            GetGoal.enabled = false;
            SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Additive);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (!pressed&&this.enabled)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        pressed = true;
                        StartCoroutine(Playtime());           
                        this.enabled = false;
                    }
                }      
            }
        }

        public IEnumerator Playtime()
        {
            Time.timeScale = 0;
            enabled = false;
            yield return SceneManager.UnloadSceneAsync("Lobby");
            GetGoal.enabled = true;
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 1;
            enabled = true;
        }
    }
}

