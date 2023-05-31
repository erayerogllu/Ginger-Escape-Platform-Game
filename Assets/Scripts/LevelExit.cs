using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelDelayTime;
    [SerializeField] AudioClip levelExitSFX;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(levelExitSFX, Camera.main.transform.position);
            StartCoroutine(LoadNextLevel());

        }

        IEnumerator LoadNextLevel()
        {
            yield return new WaitForSecondsRealtime(levelDelayTime);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }



    }

}
