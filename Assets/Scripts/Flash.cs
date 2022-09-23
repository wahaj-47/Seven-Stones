using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    public GameObject levelLoader;
    private void OnCollisionEnter(Collision other) {
        levelLoader.GetComponent<LevelLoader>().LoadNextLevel();
    }
}
