using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public void LoadNextLevel(string transition = "Start"){
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1, transition));
    }

    public void Reload(string transition = "Start"){
        AudioManager.instance.Play("Theme");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex, transition));
    }

    public void RestartGame(string transition = "Start") {
        StartCoroutine(LoadLevel(0, transition));
    }

    IEnumerator LoadLevel(int levelIndex, string transition){
        animator.SetTrigger(transition);

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(levelIndex);
    }
    
}
