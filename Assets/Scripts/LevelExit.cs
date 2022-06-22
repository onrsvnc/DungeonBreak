using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] ParticleSystem exitEffect;
    [SerializeField] float exitDelayTime = 0f;

    void OnTriggerEnter2D(Collider2D other) 
   {
       if(other.tag == "Player")
     {
        exitEffect.Play();
        GetComponent<AudioSource>().Play();
        StartCoroutine(LoadNextLevel());    
     }
   }


   IEnumerator LoadNextLevel()
   {
        yield return new WaitForSecondsRealtime(exitDelayTime);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
   }
}
