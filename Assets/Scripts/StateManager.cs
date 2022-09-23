using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelFailed;
    [SerializeField] GameObject levelCleared;
    [SerializeField] GameObject score;
    public bool won = false;
    public bool lost = false;
    public bool[] collectedStones = new bool[7];
    public int placedStones = 0;

    private void Awake() {
        if(StateManager.instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start() {
        string activeSceneName = SceneManager.GetActiveScene().name;
        if(SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex < 4) StateManager.instance.Play();
    }

    public void StartAnimation(){
        mainCamera.GetComponent<InitialAnimation>().Throw();
        player.GetComponent<InitialAnimation>().Throw();
        mainMenu.SetActive(false);
    }

    public void Play(){
        player.GetComponent<PlayerController>().ToggleMovement();
    }

    public void CollectStone(int index){
        collectedStones[index] = true;
    }

    public void PlaceStone(int index){
        collectedStones[index] = false;
        placedStones++;
    }

    public void Lose(){
        if(!lost){
            AudioManager.instance.Play("LevelLost");
            AudioManager.instance.StopPlaying("Theme");
        }
        lost = true;
        levelFailed.SetActive(true);
        score.SetActive(false);
    }

    public void Win(){
        won = true;
        levelCleared.SetActive(true);
        score.SetActive(false);
        AudioManager.instance.Play("LevelWin");
    }
}
