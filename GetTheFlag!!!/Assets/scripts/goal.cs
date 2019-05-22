using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mirror
{
    public class goal : NetworkBehaviour
    //MonoBehaviour
    {
        public bool available = true;
        public int point;
        public level rooms;
        private string[] levels;
        private List<string> levelList = new List<string>();
        private int roomsIndex;
        private bool firstScene = true;

        public GameObject flag;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (this.enabled)
            {
                if (other.CompareTag("Player"))
                {
                    if (available)
                    {
                        other.GetComponent<playerScore>().Cmd_addPoints(point);
                        Cmd_LoadNext();
                        available = false;
                    }
                }
            }

        }
        

        void Start()
        {
            levels = rooms.scenes;
            foreach(string room in levels)
            {
                levelList.Add(room);
            }
            Cmd_LoadNext();
        }

        //loads next room in rooms
        [Command]
        public void Cmd_LoadNext()
        {
            StartCoroutine(LoadNextRoom());
        }

        
        public IEnumerator LoadNextRoom()
        {
           
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(1f);
            if (!firstScene)
            {
                yield return SceneManager.UnloadSceneAsync(levelList[(roomsIndex)]);//unloads previous scene
                levelList.RemoveAt(roomsIndex);
            }
            roomsIndex = Random.Range(0, levelList.Count);
            enabled = false;
            yield return SceneManager.LoadSceneAsync(levelList[roomsIndex], LoadSceneMode.Additive);//adds new scene       
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelList[roomsIndex]));
            yield return new WaitForSecondsRealtime(1f);
            firstScene = false;
            available = true;
            Time.timeScale = 1;
            enabled = true;
        }

        private void FixedUpdate()
        {
            flag = GameObject.Find("FlagPos");
            transform.position = Vector2.Lerp(flag.transform.position, transform.position, 10 * Time.fixedDeltaTime);

        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
        }
    }
}


